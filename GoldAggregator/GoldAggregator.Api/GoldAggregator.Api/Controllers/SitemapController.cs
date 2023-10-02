using GoldAggregator.Api.Dto.Enums;
using GoldAggregator.Infrastructure.Repositories;

using Microsoft.AspNetCore.Mvc;

using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using GoldAggregator.Parser.Extension;
using Microsoft.AspNetCore.WebUtilities;

namespace GoldAggregator.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SitemapController : ControllerBase
    {
        const string rootHost = "https://goldkatalog.ru";
        const string rootHostCoins = $"{rootHost}/rossiya/monety";

        private readonly ICoinPriceRepository _coinPriceRepository;
        private readonly IFiltersRepository _filtersRepository;

        public SitemapController(ICoinPriceRepository coinPriceRepository, IFiltersRepository filtersRepository)
        {
            _coinPriceRepository = coinPriceRepository;
            _filtersRepository = filtersRepository;
        }

        [HttpGet]
        [Route("xml", Name = "generate sitemap.xml")]
        [Produces("application/xml")]
        public async Task<ActionResult<string[]>> Get()
        {
            var filters = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("CityId", "0") }.ToArray();
            var sortBy = SortCatalogCoins.None;
            var sortDirection = SortDirection.Asc;
            int? page = 1;
            int? pageSize = int.MaxValue;

            var nodes = new List<SiteMapNode>();
            var pagedListDto = await _coinPriceRepository.GetPagedCoinPricesAsync(filters, page, pageSize, sortBy, sortDirection);
            //А далее по циклу п каждой монете, берем цену с максимальной датой обновления.
            var coinCatalogIds = pagedListDto.Items.Select(item => item.Id).Distinct().ToArray();
            var coinCatalogIdParseDate = await _coinPriceRepository.GetLastUpdatedCoinCatalogsAsync(coinCatalogIds);

            #region Query coins
            foreach (var coinCatalogAndPrice in pagedListDto.Items)
            {
                if (coinCatalogAndPrice.SeoUrl != null)
                {
                    nodes.Add(new SiteMapNode
                    {
                        Url = $"{rootHostCoins}/{coinCatalogAndPrice.SeoUrl.Replace('.', '-')}",
                        Priority = 0.9,
                        LastModified = coinCatalogIdParseDate[coinCatalogAndPrice.Id],
                        Frequency = SitemapFrequency.Daily
                    });
                }
            }
            #endregion

            #region Query available filters
            // TODO availableFilters
            var availableFilters = await _filtersRepository.GetFiltersAsync();
            var parameters = availableFilters.Where(kv => kv.Key == "Weight")
                .Concat(availableFilters.Where(kv => kv.Key == "MetalType"))
                .Concat(availableFilters.Where(kv => kv.Key == "MaxPriceToSell"))
                .Concat(availableFilters.Where(kv => kv.Key == "MaxPriceToBuy"))
                .Select(x => KeyValuePair.Create(x.Key, x.Value))
                .ToArray();

            var queryFilters = new List<string>();

            for (int j = 1; j < 10; j++)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    queryFilters.Add(QueryHelpers.AddQueryString(rootHostCoins, parameters.Skip(i).Take(j)));
                }
            }
            #endregion

            foreach (var queryFilter in queryFilters)
            {
                nodes.Add(new SiteMapNode
                {
                    Url = queryFilter,
                    Priority = 0.9,
                    LastModified = null, // DateTime.Now,
                    Frequency = SitemapFrequency.Weekly
                });
            }

            var xml = GetSiteMapDocument(nodes);

            return new ContentResult
            {
                ContentType = "application/xml",
                Content = xml,
                StatusCode = 200
            };
        }

        private string GetSiteMapDocument(IEnumerable<SiteMapNode> siteMapNodes)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");

            foreach (var siteMapNode in siteMapNodes)
            {
                XElement urlxXElement =
                    new XElement(xmlns + "url",
                        new XElement(xmlns + "loc", Uri.EscapeUriString(siteMapNode.Url)), // Uri.UnescapeDataString(siteMapNode.Url)
                    siteMapNode.LastModified == null ? null :
                        new XElement(xmlns + "lastmod", siteMapNode.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                    siteMapNode.Frequency == null ? null :
                       new XElement(xmlns + "changefreq", siteMapNode.Frequency.ToString()),
                    siteMapNode.Priority == null ? null :
                       new XElement(xmlns + "priority", siteMapNode.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)));

                root.Add(urlxXElement);
            }

            XDocument document = new XDocument(root)
            {
                Declaration = new XDeclaration("1.0", "UTF-8", "no")
            };

            return document.ToString();
        }

        //private string GlueParams()
        //{

        //}

        internal class SiteMapNode
        {
            public SitemapFrequency? Frequency { get; set; }
            public DateTime? LastModified { get; set; }
            public double? Priority { get; set; }
            public string Url { get; set; }
        }

        internal enum SitemapFrequency
        {
            Never,
            Yearly,
            Monthly,
            Weekly,
            Daily,
            Hourly,
            Always
        }
    }
}
