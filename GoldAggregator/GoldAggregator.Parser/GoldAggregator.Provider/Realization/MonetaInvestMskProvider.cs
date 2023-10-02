using AngleSharp.Dom;
using AngleSharp.Html.Parser;

using GoldAggregator.Parser.Dto;
using GoldAggregator.Parser.Entities.Entities;
using GoldAggregator.Parser.Entities.Enums;
using GoldAggregator.Parser.Provider.Attributes;
using GoldAggregator.Parser.Provider.Exceptions;
using GoldAggregator.Parser.Extension;
using GoldAggregator.Parser.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldAggregator.Parser.Provider.Realization
{
    [ProviderDescription(Host = "https://msk.monetainvest.ru")]
    public class MonetaInvestMskProvider : BaseSiteProvider, ISiteParsingProvider
    {
        private readonly ILogger<MonetaInvestSpbProvider> _logger;
        private readonly IHttpClient _httpClient;
        private readonly string _rootHost;
        private readonly IConfiguration _configuration;

        public MonetaInvestMskProvider(
            ILogger<MonetaInvestSpbProvider> logger,
            IHttpClient httpClient,
            IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _rootHost = GetRootHost();
            _configuration = configuration;
        }

        public async Task<IEnumerable<Url>> ParseAndGetUrlsAsync()
        {
            var now = DateTime.Now;
            var values = new string[] {
                "https://msk.monetainvest.ru/zolotye-monety",
                "https://msk.monetainvest.ru/serebryanye-monety",
                "https://msk.monetainvest.ru/platina",
               // "https://msk.monetainvest.ru/palladij"
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
            if (url.Value.Contains("zolot"))
                metalType = MetalTypeEnum.Gold;
            else if (url.Value.Contains("serebr"))
                metalType = MetalTypeEnum.Silver;
            else if (url.Value.Contains("platina"))
                metalType = MetalTypeEnum.Platinum;
            else if (url.Value.Contains("pallad"))
                metalType = MetalTypeEnum.Palladium;

            var html = await _httpClient.GetAsync(url.Value);
            var htmlParser = new HtmlParser();
            var document = htmlParser.ParseDocument(html);
            var parseDate = DateTime.Now;

            var items = document
                .QuerySelectorAll(".monet1")
                // .Where(e => this.IsAvailable(e))
                .Select(e => this.CheckAndGetCoin(e, metalType, parseDate));

            // A little time interval to avoid DDOS
            await Task.Delay(3000);

            return items;
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
                coin.PriceSpecial = GetPriceWithDiscount(e, out var priceWithDiscountError);
                coin.ParseDate = parseDate;
                coin.StartMiningYear = GetStartMiningYear(e, out var startMiningYearError);
                coin.EndMiningYear = GetEndMiningYear(e, out var endMiningYearError);
                coin.MetalType = (int)metalType;
                // coin.Nomination = GetNomination(e, out var nominationError);
                coin.Weight = GetWeight(e, out var weightError);
                coin.Error = titleError + urlError + priceToBuyError + priceToSellError
                            + priceWithDiscountError + startMiningYearError + endMiningYearError
                            + weightError;
            }
            catch (Exception ex)
            {
                // Что-то более серьезное запишем.
                coin.Error = ex.Message;
            }

            return coin;
        }

        #region Helpers

        private bool IsAvailable(IElement e)
        {
            var text = e.QuerySelector(".netvn")?.InnerHtml;

            return text?.Contains("Нет в наличии") ?? true;
        }

        private string GetTitle(IElement e, out string error)
        {
            error = "";

            var name = e.QuerySelector(".mon-name")?.TextContent ?? "";
            if (name.IsEmpty()) error = "Title is empty;";

            return name;
        }

        // TODO можно сделать общий метод в базе
        private string GetUrl(IElement e, out string error)
        {
            error = "";

            var a = e.QuerySelector("a");
            var hasHref = a.HasAttribute("href") && !a.Attributes["href"].Value.IsEmpty();

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
        }

        private decimal? GetPriceToBuy(IElement e, out string error)
        {
            error = "";

            try
            {
                var text = e.QuerySelector(".mon-sale")?.TextContent ?? "";
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
                var text = e.QuerySelector(".mon-buy")?.TextContent ?? "";
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
        private decimal? GetPriceWithDiscount(IElement e, out string error)
        {
            error = "";

            return null;
        }

        private DateTime? GetStartMiningYear(IElement e, out string error)
        {
            error = "";
            var yearText = e.QuerySelector(".mon-year1")?.TextContent ?? "";
            var year = GetStartingYearFromText(yearText);

            if (string.IsNullOrEmpty(year))
            {
                error = "Start year is empty;";
                return null;
            }

            return DateTime.Parse("01.01." + year);
        }

        private DateTime? GetEndMiningYear(IElement e, out string error)
        {
            error = "";
            var yearText = e.QuerySelector(".mon-year2")?.TextContent ?? "";
            var year = GetEndingYearFromText(yearText);

            if (string.IsNullOrEmpty(year))
            {
                error = "End year is empty;";
                return null;
            }

            return DateTime.Parse("31.12." + year);
        }

        private decimal GetWeight(IElement e, out string error)
        {
            error = "";

            var weightText = e.QuerySelectorAll(".dops-infos2")
            .Where(element => element.QuerySelector(".dzag")?.TextContent == "Вес").FirstOrDefault()?.TextContent ?? "";

            if (base.GetWeightFromText(weightText, out var weight))
            {
                return base.ToDecimal(weight);
            };

            error = $"Can't find or define weight from  weightText '{weightText}'";

            return 0;
        }

        #endregion
    }
}
