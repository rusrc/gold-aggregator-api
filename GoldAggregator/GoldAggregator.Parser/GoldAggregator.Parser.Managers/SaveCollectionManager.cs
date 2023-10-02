using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using GoldAggregator.Parser.DbContext;
using GoldAggregator.Parser.Dto;
using GoldAggregator.Parser.Entities.Entities;
using GoldAggregator.Parser.Entities.Enums;
using GoldAggregator.Parser.Exceptions;
using GoldAggregator.Parser.Logger;

using GoldAggregator.Parser.Provider;
using GoldAggregator.Parser.Extension;
using GoldAggregator.Parser.Provider.Map;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GoldAggregator.Parser.Managers
{
    public class SaveCollectionManager : ISaveItemsManager
    {
        private readonly ISiteParsingProvider _siteProvider;
        private readonly ILogger<SaveCollectionManager> _logger;
        private readonly ParserDbContext _context;
        private readonly DefaultMap _mapService;

        public SaveCollectionManager(
            ISiteParsingProvider siteProvider,
            ILogger<SaveCollectionManager> logger,
            ParserDbContext context,
            DefaultMap mapService)
        {
            _siteProvider = siteProvider;
            _logger = logger;
            _context = context;
            _mapService = mapService;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            var providerName = _siteProvider.GetType().Name;
            var dealer = _context.Dealers
                                .Include(d => d.Urls)
                                .SingleOrDefault(d => d.ProviderName == providerName);


            if (dealer == null) throw new NotFoundException(providerName, "Can't find dealer");
            if (dealer.Urls == null) throw new NotFoundException(providerName, "No urls");
            // Логирование скорости 
            using var p = new TimePointer<SaveCollectionManager>($"provider '{providerName}'", _logger);
            var newCoinsDto = new List<CoinDto>();

            try
            {
                foreach (var url in dealer.Urls.Where(u => u.Status == Status.ReadyToParse))
                {
                    _logger.LogInformation($"Start parsing {url.Value}, provider: {providerName}");

                    var coinsDto = await _siteProvider.ParseAndGetItemsAsync(url); p.Add($"[NEXT] parsed {url.Value}");
                    newCoinsDto.AddRange(coinsDto);
                }
                //Зачистка от перчаток/луп/подарочных коробок и всякого хлама               
                var newCoinsDtoTemp = newCoinsDto.Where(c => c.Weight != 0 && // без веса 
                (c.PriceToBuy != null || c.PriceToSell != null)) //без цены  
                .Distinct().ToList();

                //Убираем дубли, если на разных страницах монеты пересекаются
                newCoinsDto.Clear();
                foreach (var coin in newCoinsDtoTemp)
                {
                    if (!newCoinsDto.Any(c => c.Url == coin.Url)) { 
                        newCoinsDto.Add(coin);
                    }                   
                }

                //линкуем спаршенные цены и монеты из каталога
                var newCoins = await _mapService.MapParsedCoinsToCatalog(newCoinsDto, dealer.Id);

                if (newCoins.Any())
                {
                    //Удаляем старые цены из каталога цен
                    var oldCoins = await _context.CoinPrices.Where(c => c.DealerId == dealer.Id).ToArrayAsync();
                    _context.CoinPrices.RemoveRange(oldCoins);
                    //Сохраняем в таблицу Coin только самые свежие записи
                    await _context.CoinPrices.AddRangeAsync(newCoins);
                    //Сохраняем для истории все цены в таблицу CoinHistory
                    var coinsPriceHistory = newCoins.Select(c => new CoinPriceHistory()
                    {
                        CoinFromCatalogId = c.CoinFromCatalogId,
                        DealerId = dealer.Id,
                        ParseDate = c.ParseDate,
                        PriceSpecial = c.PriceSpecial,
                        PriceSpecialPerGram = c.PriceSpecialPerGram,
                        PriceToBuy = c.PriceToBuy,
                        PriceToBuyPerGram = c.PriceToBuyPerGram,
                        PriceToSell = c.PriceToSell,
                        PriceToSellPerGram = c.PriceToSellPerGram,
                        Url = c.Url,
                        Title = c.Title,
                    });

                    // TODO пока статус не меням, чтобы сто раз не обновлять в базе для теста.
                    // url.Status = Status.Active;

                    await _context.CoinsPriceHistory.AddRangeAsync(coinsPriceHistory);
                    await _context.SaveChangesAsync();
                }
            }
            #region Exceptions
            catch (ItemSoldException ex)
            {
                // TODO AddError
                p.Add($"[Error] {ex.Message}");
                // SaveException(url, ex, context, Status.NotFound);
            }
            catch (DealershipNotFoundException ex)
            {
                // TODO AddError
                p.Add($"[Error] {ex.Message}");
                // SaveDealership(item, context);
                // SaveException(url, ex, context, Status.WaitToCheck);
            }
            catch (NotFoundException ex)
            {
                // TODO AddError
                p.Add($"[Error] {ex.Message}");
                // SaveException(url, ex, context, Status.NotFound);
            }
            catch (ProviderException ex)
            {
                // TODO AddError
                p.Add($"[Error] {ex.Message}");
                // SaveException(url, ex, context, Status.WaitToCheck);
            }
            #endregion
            catch (Exception ex)
            {
                // TODO AddError
                _logger.LogCritical(ex, ex.Message);
                p.Add($"[Error] {ex.Message}");
                // SaveException(url, ex, context);
            }
        }

        private IEnumerable<CoinDto> SaveEndRemoveFaildCoins(IEnumerable<CoinDto> coinsDto)
        {
            var failedCoins = coinsDto.Where(x => !x.Error.IsEmpty());
            // TODO SaveEndRemoveFaildCoins
            // _context.FailedCoins.AddRange(failedCoins);
            return coinsDto.Where(c => c.Error.IsEmpty());
        }
    }
}
