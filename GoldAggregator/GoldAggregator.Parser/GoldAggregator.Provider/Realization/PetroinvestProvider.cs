using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using GoldAggregator.Parser.Dto;
using GoldAggregator.Parser.Entities.Entities;
using GoldAggregator.Parser.Entities.Enums;
using GoldAggregator.Parser.Provider.Attributes;
using GoldAggregator.Parser.Provider.Exceptions;
using GoldAggregator.Parser.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldAggregator.Parser.Provider.Realization
{
    [ProviderDescription(Host = "http://petroinvest.ru")]
    public class PetroinvestProvider : BaseSiteProvider, ISiteParsingProvider
    {
        private readonly ILogger<PetroinvestProvider> _logger;
        private readonly IHttpClient _httpClient;
        private readonly string _rootHost;
        private readonly IConfiguration _configuration;

        public PetroinvestProvider(
            ILogger<PetroinvestProvider> logger,
            IHttpClient httpClient,
            IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _rootHost = GetRootHost();
            _configuration = configuration;
        }

        //У этого дилера нет монет без цен!!!
        public async Task<IEnumerable<Url>> ParseAndGetUrlsAsync()
        {
            var now = DateTime.Now;
            var values = new string[] {
                "http://petroinvest.ru/catalog/gold/",
                "http://petroinvest.ru/catalog/silver/filter/money_probe-is-exaustp7-or-s5cbiki1/apply/"
            };

            var urls = values.Select(value => new Url
            {
                CreateDate = now,
                ModifiedDate = now,
                Value = value,
                Status = Status.Active
            });

            return urls;
        }

        public async Task<IEnumerable<CoinDto>> ParseAndGetItemsAsync(Url url)
        {
            var metalType = MetalTypeEnum.Undefined;
            if (url.Value.Contains("gold"))
                metalType = MetalTypeEnum.Gold;
            else if (url.Value.Contains("silver"))
                metalType = MetalTypeEnum.Silver;

            var parseDate = DateTime.Now;
            bool hasAnyCoinsWithPrices = false;
            var pageNumber = 1;
            var urlWithPageNumber = url.Value;

            var allCoins = new List<CoinDto>();
            var hasContinue = true;
            do
            {
                var html = await _httpClient.GetAsync(urlWithPageNumber);
                var document = new HtmlParser().ParseDocument(html);

                var coins = document
                .QuerySelectorAll(".product-item")
                .Select(e => this.CheckAndGetCoin(e, metalType, parseDate));

                hasAnyCoinsWithPrices = coins.Any(c => c.PriceToBuy != null || c.PriceToSell != null);


                //Если есть цены, и они не повторяются, то идем дальше
                if (!hasAnyCoinsWithPrices ||
                 (allCoins.Count() > 0 && allCoins.Any(c => c.Title == coins.First(c => c.PriceToBuy != null || c.PriceToSell != null).Title))
                 )
                {
                    hasContinue = false;
                }

                allCoins.AddRange(coins.Where(c => c.PriceToBuy != null || c.PriceToSell != null)); //добавляем только монеты с ценами

                pageNumber++;
                urlWithPageNumber = url.Value + $"?PAGEN_1={pageNumber}";


                // Подождем 2 сек. чтобы не заDDOS-ить источник
                _logger.LogInformation($"Wait for 2 sec. To avoid DDOS actions. Url: '{urlWithPageNumber}'");
                await Task.Delay(2000);
            } while (hasContinue); //Покаесть хоть одна монета с ценой покупки или продажи, парсим следующие страницы

            return allCoins;
        }

        private CoinDto CheckAndGetCoin(IElement e, MetalTypeEnum metalType, DateTime parseDate)
        {
            var coin = new CoinDto();

            try
            {
                string title = GetTitle(e, out var titleError).Trim();

                coin.Title = title;
                coin.Url = GetUrl(e, out var urlError);
                coin.PriceToBuy = GetPriceToBuy(e, out var priceToBuyError);
                coin.PriceToSell = GetPriceToSell(e, out var priceToSellError);
                coin.PriceSpecial = null;
                coin.ParseDate = parseDate;
                //coin.StartMiningYear = GetStartMiningYear(e, out var startMiningYearError);
                //coin.EndMiningYear = GetEndMiningYear(e, out var endMiningYearError);
                coin.MetalType = (int)metalType;
                // coin.Nomination = GetNomination(e, out var nominationError);
                coin.Weight = GetWeight(title, out var weightError);
                coin.Error = titleError + urlError + priceToBuyError + priceToSellError
                            //startMiningYearError + endMiningYearError
                            //+ weightError
                            ;
            }
            catch (Exception ex)
            {
                // Что-то более серьезное запишем.
                coin.Error = ex.Message;
            }

            return coin;
        }

        #region Helpers

        private string GetTitle(IElement e, out string error)
        {
            error = "";
            var a = e.QuerySelector(".product-item-title");

            try
            {
                return a.TextContent;
            }
            catch
            {
                error = "Title is empty;";
            }

            return "";
        }

        // TODO можно сделать общий метод в базе
        private string GetUrl(IElement e, out string error)
        {
            error = "";

            try
            {
                var a = e.QuerySelector(".product-item-title a");

                var href = a.Attributes["href"]?.Value;
                if (!href.Contains(_rootHost))
                {
                    href = $"{_rootHost}{href}";
                }
                return href;
            }
            catch
            {
                error = "Url not found;";
                return "";
            }
        }

        private decimal? GetPriceToBuy(IElement e, out string error)
        {
            error = "";

            try
            {
                var text = e.QuerySelector("dd b")?.TextContent ?? "";
                if (text.Any(char.IsDigit))
                {
                    var price = this.GetPriceFromText(text);
                    return price;
                }
            }
            catch (PriceNotFoundException)
            {
                error = "price not found;";
            }

            return null;
        }
        private decimal? GetPriceToSell(IElement e, out string error)
        {
            error = "";

            try
            {
                var text = e.QuerySelector(".product-item-price-current")?.TextContent ?? "";
                if (text.Any(char.IsDigit))
                {
                    var price = this.GetPriceFromText(text);
                    return price;
                }
            }
            catch (PriceNotFoundException)
            {
                error = "price not found;";
            }

            return null;            
        }

        private decimal GetWeight(string title, out string error)
        {
            error = "";
            if (base.GetWeightFromText(title, out var weight))
            {
                return base.ToDecimal(weight);
            };

            error = $"Can't find or define weight from title '{title}'";

            return 0;

        }

        #endregion

    }
}
