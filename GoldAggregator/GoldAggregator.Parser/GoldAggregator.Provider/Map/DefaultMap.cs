using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldAggregator.Parser.DbContext;
using GoldAggregator.Parser.Entities.Entities;
using GoldAggregator.Parser.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GoldAggregator.Parser.Entities.Enums;

namespace GoldAggregator.Parser.Provider.Map
{
    // TODO можно перенести в GoldAggregator.Parser.Mapper.
    // Если будет много маперов в этом будет смысл.
    public class DefaultMap
    {
        private readonly ILogger<DefaultMap> _logger;
        private readonly ParserDbContext _context;
        public DefaultMap(ParserDbContext context, ILogger<DefaultMap> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Прогоняем спаршенные монеты и проставляем в каждом CoinFromCatalogId нашей монеты из каталога
        /// </summary>
        /// <param name="pparsedCoins"></param>
        /// <param name="dealerId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CoinPrice>> MapParsedCoinsToCatalog(IEnumerable<CoinDto> parsedCoins, int? dealerId)
        {
            var matchedDealerCoins = await _context.DillerCoinMaps.Where(c => c.DealerId == dealerId).ToArrayAsync();
            var catalogCoins = await _context.CoinCatalogs.ToArrayAsync();

            var result = new List<CoinPrice>();
            var unMappedCoins = new List<CoinDto>();
            //сопоставляем монету с монетой из каталог, сперва прогоняем по спец сопоставлениям от диллеров
            foreach (var coinDto in parsedCoins)
            {
                var coinFromCatalog = matchedDealerCoins.FirstOrDefault(
                d => d.MatchedTitle.ToLower() == coinDto.Title.ToLower() ||
                d.Url.ToLower() == coinDto.Url
                );

                if (coinFromCatalog != null)
                {
                    var coinCatalog = catalogCoins.FirstOrDefault(c => c.Id == coinFromCatalog.CoinFromCatalogId);
                    var coin = MapCoin(coinDto, coinFromCatalog.CoinFromCatalogId, dealerId, coinCatalog);
                    result.Add(coin);
                }
                else
                {
                    unMappedCoins.Add(coinDto);
                }
            }


            ////Если остались монеты без сопоставления с каталогом, попробуем сопоставить по наименованию, весу и году и т.д         
            if (unMappedCoins.Any())
             {                
                var allSeoKeyWords = new Dictionary<string, Dictionary<string, int>>();

                foreach (var coinDto in unMappedCoins)
                {
                    int? coinFromCatalogId = null;

                    //Если не нашли монету в каталоге по имени ищем по SeoKetWords
                    if (coinFromCatalogId == null)
                    {
                        var metalType = (MetalTypeEnum)coinDto.MetalType;
                        var weight = coinDto.Weight;
                        var weightAndMetalTypeKey = $"{weight} + {metalType}";

                        //Если словарь уже есть, то просто ищем, а если нет, то создаем и сохраняем для последующего маппинга
                        Dictionary<string, int> seoKeyWordsByWeightAndMetalType;
                        if (!allSeoKeyWords.TryGetValue(weightAndMetalTypeKey, out seoKeyWordsByWeightAndMetalType))
                        {
                          //Получает словарь где ключ это seoKeyWord, а значение это Id монеты
                                var seoKeyWords = catalogCoins
                                    .Where(coin => coin.Weight == weight && coin.MetalType == metalType && coin.SeoKeyWords != null)
                                    .SelectMany(coin => coin.SeoKeyWords.Split(','), (coin, seoKeyWord) => new { seoKeyWord, coin.Id })
                                    .Select(coinAndSeoKeyWord => new { seoKeyWord = coinAndSeoKeyWord.seoKeyWord.Trim(), coinId = coinAndSeoKeyWord.Id })
                                    .Distinct()
                                    .ToDictionary(coinAndSeoKeyWord => coinAndSeoKeyWord.seoKeyWord, coinAndSeoKeyWord => coinAndSeoKeyWord.coinId);
                                //Добавляем этот словарь в общий словарь
                                allSeoKeyWords.TryAdd(weightAndMetalTypeKey, seoKeyWords);
                        }
                        if (seoKeyWordsByWeightAndMetalType == null)
                        {
                            seoKeyWordsByWeightAndMetalType = allSeoKeyWords[weightAndMetalTypeKey];
                        }

                        //Пытаемся найти seoKeyWord из Title цены? 
                        var seoKeyWordFromTitle = seoKeyWordsByWeightAndMetalType.Keys.Where(
                            key => coinDto.Title.ToLower().Contains(key.ToLower()))
                            .OrderByDescending(k=>k.Length) // сортируем по длинне слова совпавших seoKeyWords 
                            .FirstOrDefault(); // и берем самое длинное слово, что бы убрать совпадения менььшей длинны
                           //Пример  Британия и Великобритания, выбираем seoKeyWords Великобритания

                        if (seoKeyWordFromTitle != null)
                        {
                            //Получаем Id монеты по найденному seoKeyWordFromTitle
                            coinFromCatalogId = seoKeyWordsByWeightAndMetalType[seoKeyWordFromTitle];
                        }
                    }
                    var coinCatalog = catalogCoins.FirstOrDefault(c => c.Id == coinFromCatalogId);
                    var coin = MapCoin(coinDto, coinFromCatalogId, dealerId, coinCatalog);
                    result.Add(coin);
                }
            }

            return result;
        }

        public static CoinPrice MapCoin(CoinDto coinDto, int? coinFromCatalogId, int? dealerId, CoinCatalog coinCatalog)
        {
            decimal? priceSpecialPerGram;
            decimal? priceToBuyPerGram;
            decimal? priceToSellPerGram;
            //Для точности берем вес из каталога монет
            if (coinCatalog != null)
            {
                priceSpecialPerGram = coinDto.PriceSpecial != null ? coinDto.PriceSpecial / coinCatalog.Weight : null;
                priceToBuyPerGram = coinDto.PriceToBuy != null ? coinDto.PriceToBuy / coinCatalog.Weight : null;
                priceToSellPerGram = coinDto.PriceToSell != null ? coinDto.PriceToSell / coinCatalog.Weight : null;
            }
            else
            {
                priceSpecialPerGram = coinDto.PriceSpecial != null ? coinDto.PriceSpecial / coinDto.Weight : null;
                priceToBuyPerGram = coinDto.PriceToBuy != null ? coinDto.PriceToBuy / coinDto.Weight : null;
                priceToSellPerGram = coinDto.PriceToSell != null ? coinDto.PriceToSell / coinDto.Weight : null;
            }           

            var coin = new CoinPrice()
            {
                ParseDate = coinDto.ParseDate,
                PriceSpecial = coinDto.PriceSpecial,
                PriceSpecialPerGram = coinDto.PriceSpecial != null ? Math.Round((decimal)priceSpecialPerGram) : null,
                PriceToBuy = coinDto.PriceToBuy,
                PriceToBuyPerGram = coinDto.PriceToBuy != null ? Math.Round((decimal)priceToBuyPerGram) : null,
                PriceToSell = coinDto.PriceToSell,
                PriceToSellPerGram = coinDto.PriceToSell != null ? Math.Round((decimal)priceToSellPerGram) : null,
                Url = coinDto.Url,
                Title = coinDto.Title,
                Error = coinDto.Error,

                // Присваиваем доп идентификаторы
                DealerId = (int)dealerId,
                // Привязываем спаршенную монету с ценой к каталогу монет
                CoinFromCatalogId = coinFromCatalogId
            };

            return coin;
        }
    }
}
