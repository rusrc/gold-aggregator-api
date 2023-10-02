using AngleSharp.Dom;
using AngleSharp.Html.Parser;

using GoldAggregator.Parser.Entities.Entities;
using GoldAggregator.Parser.Dto;
using GoldAggregator.Parser.Provider.Attributes;
using GoldAggregator.Parser.Provider.Exceptions;
using GoldAggregator.Parser.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using GoldAggregator.Parser.Entities.Enums;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace GoldAggregator.Parser.Provider.Realization
{
    [ProviderDescription(Host = "http://zoloto-piter.ru")]
    public class ZolotoPiterRuProvider : BaseSiteProvider, ISiteParsingProvider
    {
        private readonly ILogger<ZolotoPiterRuProvider> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClient _httpClient;
        private readonly string _rootHost;

        public ZolotoPiterRuProvider(
            ILogger<ZolotoPiterRuProvider> logger,
            IConfiguration configuration,
            IHttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration;
            _rootHost = GetRootHost();
        }

        public async Task<IEnumerable<Url>> ParseAndGetUrlsAsync()
        {
            var now = DateTime.Now;
            var values = new string[] {
                "http://zoloto-piter.ru/gold"
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
            var allCoins = new List<CoinDto>();
            var html = await _httpClient.GetAsync(url.Value);
            var document = new HtmlParser().ParseDocument(html);
            var parseDate = DateTime.Now;

            var coins = document
               .QuerySelectorAll(".product-item-container")
               .Select(e => this.CheckAndGetCoin(e, parseDate));

            allCoins.AddRange(coins.Where(c => c.PriceToBuy != null || c.PriceToSell != null)); //добавляем только монеты с ценами

            return allCoins;
        }

        private CoinDto CheckAndGetCoin(IElement e, DateTime parseDate)
        {

            var coin = new CoinDto();

            try
            {
                string title = GetTitle(e, out var titleError);

                coin.Title = title;
                coin.Url = GetUrl(e, out var urlError);
                coin.PriceToBuy = GetPriceToBuy(e, out var priceToBuyError);
                coin.PriceToSell = GetPriceToSell(e, out var priceToSellError);
                coin.PriceSpecial = GetPriceSpecial(e, out var priceSpecialError);
                coin.ParseDate = parseDate;
                coin.StartMiningYear = GetStartMiningYear(e, out var startMiningYearError);
                coin.EndMiningYear = GetEndMiningYear(e, out var endMiningYearError);
                coin.MetalType = (int)MetalTypeEnum.Gold;
                //coin.Nomination = GetNomination(e, out var nominationError);
                coin.Weight = GetWeight(e, out var weightError);

                coin.Error = titleError + urlError + priceToBuyError + priceToSellError
                            + priceSpecialError + startMiningYearError + endMiningYearError
                            +  weightError;
            }
            catch (Exception ex)
            {
                // Что-то более серьезное запишем.
                coin.Error = ex.Message;
            }

            return coin;
        }


        #region Helpers

        private DateTime? GetStartMiningYear(IElement e, out string error)
        {
            error = "";

            var text = e.QuerySelectorAll(".product-data-li > span")
                        .Where(e => !string.IsNullOrEmpty(e.Text()))
                        // Дебильная структура должно прийти "Вес: 31.1035г", "Номинал: 100", "Год выпуска : 2022"
                        .Where(e => e.Text().Contains("Год выпуска", StringComparison.InvariantCultureIgnoreCase))
                        .Select(e => e.Parent?.Text())
                        .FirstOrDefault();

            if (!string.IsNullOrEmpty(text))
            {
                var year = Regex.Match(text, @"(?<year>\d{4})").Groups["year"]?.Value;
                return DateTime.Parse("01.01." + year);
            }
            error = "staring year is empty;";
            return null;
        }

        private DateTime? GetEndMiningYear(IElement e, out string error)
        {
            error = "";

            return null;
        }

        private string GetTitle(IElement e, out string error)
        {
            error = string.Empty;

            var name = e.QuerySelector(".right-block h4 a")?.TextContent ?? "";
            if (string.IsNullOrEmpty(name))
            {
                error = "Title is empty;";
            }

            return name;
        }

        private string GetUrl(IElement e, out string error)
        {
            error = "";

            var a = e.QuerySelector(".right-block h4 a");
            var hasHref = a.HasAttribute("href") && !string.IsNullOrEmpty(a.Attributes["href"]?.Value);

            if (hasHref)
            {
                var href = a.Attributes["href"]?.Value;
                if (!href.Contains(_rootHost))
                {
                    href = $"{_rootHost}{href}";
                }
                return href;
            }
            else
            {
                error = "Url not found;";
                return "";
            }
            // throw new UrlNotFoundException(ProviderName);
        }

        private decimal GetWeight(IElement e, out string error)
        {
            error = "";

            var text = e.QuerySelector(".right-block h4 a")?.TextContent ?? "";

            if (base.GetWeightFromText(text, out var weight))
            {
                return ToDecimal(weight);
            };

            error = $"Can't get weight from title '{text}'";

            return 0;
        }

        private decimal? GetPriceToBuy(IElement e, out string error)
        {
            error = string.Empty;

            try
            {
                var text = e.QuerySelectorAll(".right-block .price span")
                            .FirstOrDefault(e => e.Text().Contains("Покупка", StringComparison.InvariantCultureIgnoreCase))?
                            .Text();

                if (text.Any(char.IsDigit))
                {
                    var price = this.GetPriceFromText(text);
                    return price;
                }
            }
            catch (PriceNotFoundException)
            {
                error = $"{nameof(CoinPrice.PriceToSell)} not found;";
            }

            return null;
        }
        private decimal? GetPriceToSell(IElement e, out string error)
        {
            error = string.Empty;

            try
            {
                var text = e.QuerySelectorAll(".right-block .price span")
                             .FirstOrDefault(e => e.Text().Contains("Продажа", StringComparison.InvariantCultureIgnoreCase))?
                             .Text();

                if (text.Any(char.IsDigit))
                {
                    var price = this.GetPriceFromText(text);
                    return price;
                }
            }
            catch (PriceNotFoundException)
            {
                error = $"{nameof(CoinPrice.PriceToSell)} not found;";
            }

            return null;
        }
        private decimal? GetPriceSpecial(IElement e, out string error)
        {
            error = string.Empty;

            try
            {
                var text = e.QuerySelectorAll(".right-block .price span")
                              .FirstOrDefault(e => e.Text().Contains("Клубная цена", StringComparison.InvariantCultureIgnoreCase))?
                              .Text();

                if (text.Any(char.IsDigit))
                {
                    var price = this.GetPriceFromText(text);
                    return price;
                }
            }
            catch (PriceNotFoundException ex)
            {
                error = $"{nameof(CoinPrice.PriceToSell)} not found;";
            }

            return null;
        }

        #endregion
    }
}
