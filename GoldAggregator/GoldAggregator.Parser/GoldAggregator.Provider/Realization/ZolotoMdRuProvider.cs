using AngleSharp.Dom;
using AngleSharp.Html.Parser;

using GoldAggregator.Parser.Dto;
using GoldAggregator.Parser.Entities.Entities;
using GoldAggregator.Parser.Entities.Enums;
using GoldAggregator.Parser.Exceptions;
using GoldAggregator.Parser.Provider.Attributes;
using GoldAggregator.Parser.Provider.Exceptions;
using GoldAggregator.Parser.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace GoldAggregator.Parser.Provider.Realization;


[ProviderDescription(Host = "https://zoloto-md.ru")]
public class ZolotoMdRuProvider : BaseSiteProvider, ISiteParsingProvider
{
    private readonly ILogger<ZolotoMdRuProvider> _logger;
    private readonly IHttpClient _httpClient;
    private readonly string _rootHost;
    private readonly IConfiguration _configuration;

    public ZolotoMdRuProvider(
        ILogger<ZolotoMdRuProvider> logger,
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
        var url = $"{_rootHost}/bullion-coins?page=1&limit=100&metal=%D0%97%D0%BE%D0%BB%D0%BE%D1%82%D0%BE%2C%D0%9F%D0%BB%D0%B0%D1%82%D0%B8%D0%BD%D0%B0%2C%D0%A1%D0%B5%D1%80%D0%B5%D0%B1%D1%80%D0%BE&available=0";

        var urls = new List<Url>();
        urls.Add(new Url
         {

             CreateDate = now,
             ModifiedDate = now,
             Value = url,
             Status = Status.Active
         });
        return urls;
    }

    public async Task<IEnumerable<CoinDto>> ParseAndGetItemsAsync(Url url)
    {
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
                .QuerySelectorAll(".product-list_item")
                .Select(e => this.CheckAndGetCoin(e, parseDate));

            hasAnyCoinsWithPrices = coins.Any(c => c.PriceToBuy != null || c.PriceToSell != null);

            //Если есть цены, и они не повторяются, то идем дальше
            if (!hasAnyCoinsWithPrices || 
                (allCoins.Count()>0 && allCoins.Any(c => c.Title == coins.First(c => c.PriceToBuy != null || c.PriceToSell != null).Title))
                )
            {
                hasContinue = false;
            }

            allCoins.AddRange(coins.Where(c => c.PriceToBuy != null || c.PriceToSell != null)); //добавляем только монеты с ценами

            pageNumber++;
            var pageNumberString = $"page={pageNumber}";
            urlWithPageNumber = url.Value.Replace("page=1", pageNumberString);
           
            // Подождем 2 сек. чтобы не заDDOS-ить источник
            _logger.LogInformation($"Wait for 2 sec. To avoid DDOS actions. Url: '{urlWithPageNumber}'");
            await Task.Delay(2000);
        } while (hasContinue); //Покаесть хоть одна монета с ценой покупки или продажи, парсим следующие страницы

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
            coin.PriceSpecial = GetPriceWithDiscount(e, out var priceWithDiscountError);
            coin.ParseDate = parseDate;
            coin.StartMiningYear = GetStartMiningYear(title, out var startMiningYearError);
            coin.EndMiningYear = GetEndMiningYear(title, out var endMiningYearError);
            coin.MetalType = (int)GetMetalType(title, out var metalTypeError);
            // coin.Nomination = GetNomination(e, out var nominationError);
            coin.Weight = GetWeight(title, out var weightError);
            coin.Error = titleError + urlError + priceToBuyError + priceToSellError
                        + priceWithDiscountError + startMiningYearError + endMiningYearError
                        + metalTypeError + weightError;
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

        var name = e.QuerySelector("p")?.TextContent ?? "";
        if (string.IsNullOrEmpty(name))
        {
            error = "Title is empty;";
        }

        return name;
    }

    private decimal? GetPriceToBuy(IElement e, out string error)
    {
        error = "";

        try
        {
            var text = e.QuerySelector(".product_price .js-price-buyout")?.TextContent ?? "";
            if (text.Any(char.IsDigit))
            {
                var price = this.GetPriceFromText(text);
                return price;
            }
        }
        catch (PriceNotFoundException ex)
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
            var text = e.QuerySelector(".product_price .js-price")?.TextContent ?? "";
            if (text.Any(char.IsDigit))
            {
                var price = this.GetPriceFromText(text);
                return price;
            }            
        }
        catch (PriceNotFoundException ex)
        {
            error = "price not found;";
        }

        return null;
    }
    private decimal? GetPriceWithDiscount(IElement e, out string error)
    {
        error = "";

        try
        {
            var text = e.QuerySelector(".product_price__club .js-price-club")?.TextContent ?? "";
            if (text.Any(char.IsDigit))
            {
                var price = this.GetPriceFromText(text);
                return price;
            }         
        }
        catch (PriceNotFoundException ex)
        {
            error = "price not found;";
        }

        return null;
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
    private DateTime? GetStartMiningYear(string title, out string error)
    {
        error = "";
        var year = base.GetStartingYearFromText(title);

        if (string.IsNullOrEmpty(year))
        {
            error = "staring year is empty;";
            return null;
        }

        return DateTime.Parse("01.01." + year);
    }

    private DateTime? GetEndMiningYear(string title, out string error)
    {
        error = "";
        var year = base.GetEndingYearFromText(title);

        if (string.IsNullOrEmpty(year))
        {
            error = "staring year is empty;";
            return null;
        }

        return DateTime.Parse("01.01." + year);
    }
    private MetalTypeEnum GetMetalType(string title, out string error)
    {
        error = "";

        var metalType = base.GetMetalType(title);
        if (metalType != null)
        {
            return (MetalTypeEnum)metalType;
        }

        error = $"Can't find metal type. The text '{title}'";
        return MetalTypeEnum.Undefined;
    }
    private string GetNomination(IElement e, out string error)
    {
        error = "";

        throw new NotImplementedException();
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
