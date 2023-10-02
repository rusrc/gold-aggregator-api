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
    [ProviderDescription(Host = "https://www.troystandart.ru")]
    public class TroyStandartProvider : BaseSiteProvider, ISiteParsingProvider
    {
        private readonly ILogger<TroyStandartProvider> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClient _httpClient;
        private readonly string _rootHost;

        public TroyStandartProvider(
            ILogger<TroyStandartProvider> logger,
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
                "https://www.troystandart.ru/catalog/coins/filter/metall-is-39dc39f9b49ef511677bca7bf5f886d8-or-efdea8f5dcd02f951cb892c2cdea1f26-or-a9c272b76628608028c8a468a8c6baaa/pure_list-is-634ec472a464898e2a8406c59b49f252/apply/"
                ,"https://www.troystandart.ru/catalog/coins/filter/metall-is-39dc39f9b49ef511677bca7bf5f886d8-or-efdea8f5dcd02f951cb892c2cdea1f26-or-a9c272b76628608028c8a468a8c6baaa/pure_list-is-db1154e5963853d5cffd0f785689bab2/apply/"
                ,"https://www.troystandart.ru/catalog/coins/filter/metall-is-39dc39f9b49ef511677bca7bf5f886d8-or-efdea8f5dcd02f951cb892c2cdea1f26-or-a9c272b76628608028c8a468a8c6baaa/pure_list-is-e0fae884b319956cfefb8ce782a25331/apply/"
                ,"https://www.troystandart.ru/catalog/coins/filter/metall-is-39dc39f9b49ef511677bca7bf5f886d8-or-efdea8f5dcd02f951cb892c2cdea1f26-or-a9c272b76628608028c8a468a8c6baaa/pure_list-is-2dac8d55e9cc537a4780e9665b29ade9/apply/"
                ,"https://www.troystandart.ru/catalog/coins/filter/metall-is-39dc39f9b49ef511677bca7bf5f886d8-or-efdea8f5dcd02f951cb892c2cdea1f26-or-a9c272b76628608028c8a468a8c6baaa/pure_list-is-c81a48ace46ea5efe3f07577e66ea8d0/apply/"
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
            decimal weight = 0;
            if (url.Value.Contains("634ec472a464898e2a8406c59b49f252"))
                weight = 3.11m;
            else if (url.Value.Contains("db1154e5963853d5cffd0f785689bab2"))
                weight = 7.78m;
            else if (url.Value.Contains("e0fae884b319956cfefb8ce782a25331"))
                weight = 15.55m;
            else if (url.Value.Contains("2dac8d55e9cc537a4780e9665b29ade9"))
                weight = 31.1m;
            else if (url.Value.Contains("c81a48ace46ea5efe3f07577e66ea8d0"))
                weight = 62.2m;

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
                .QuerySelectorAll(".item")
                .Select(e => this.CheckAndGetCoin(e, weight, parseDate));

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

        private CoinDto CheckAndGetCoin(IElement e, decimal weight, DateTime parseDate)
        {

            var coin = new CoinDto();

            try
            {
                string title = GetTitle(e, out var titleError);

                coin.Title = title;
                coin.Url = GetUrl(e, out var urlError); // Зададим после этого метода через url.Value                
                coin.PriceToBuy = GetPriceToBuy(e, out var priceToBuyError);
                coin.PriceToSell = GetPriceToSell(e, out var priceToSellError);
                coin.PriceSpecial = null;
                coin.ParseDate = parseDate;
                coin.StartMiningYear = null;
                coin.StartMiningYear = null;
                coin.EndMiningYear = null;
                coin.MetalType = GetMetalType(e, out var metalTypeError);
                //coin.Nomination = GetNomination(e, out var nominationError);
                coin.Weight = weight != 0 ? weight : GetWeight(e, out var weightError);

                coin.Error = titleError + urlError + priceToBuyError + priceToSellError
                            + metalTypeError;
            }
            catch (Exception ex)
            {
                // Что-то более серьезное запишем.
                coin.Error = ex.Message;
            }

            return coin;
        }


        #region Helpers

        private int GetMetalType(IElement e, out string error)
        {
            error = "";

            var text = e.QuerySelector(".item_link")?.TextContent ?? "";

            var metalType = base.GetMetalType(text);
            if (metalType != null)
            {
                return (int)metalType;
            }

            error = $"Can't find metal type. The text '{text}'";
            return (int)MetalTypeEnum.Undefined;
        }

        private decimal GetWeight(IElement e, out string error)
        {
            error = "";

            var text = e.QuerySelector(".item_link")?.TextContent ?? "";

            if (base.GetWeightFromText(text, out var weight))
            {
                return ToDecimal(weight);
            };

            error = $"Can't get weight from title '{text}'";

            return 0;
        }

        private string GetTitle(IElement e, out string error)
        {
            error = string.Empty;

            var name = e.QuerySelector(".item_link")?.TextContent ?? "";
            if (string.IsNullOrEmpty(name))
            {
                error = "Title is empty;";
            }

            return name;
        }
        private string GetUrl(IElement e, out string error)
        {
            error = "";

            var a = e.QuerySelector("a");
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

        private decimal? GetPriceToBuy(IElement e, out string error)
        {
            error = string.Empty;

            try
            {
                var text = e.QuerySelectorAll(".price_sell span")[1].Text();

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
                var text = e.QuerySelectorAll(".price_buy span")[1].Text();
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

        #endregion
    }
}
