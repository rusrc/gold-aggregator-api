using GoldAggregator.Api.Dto;
using GoldAggregator.Parser.Logger;
using GoldAggregator.Parser.Services;
using GoldAggregator.Parser.Services.Abstractions;

using Microsoft.AspNetCore.Mvc;

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GoldAggregator.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeoController : ControllerBase
    {
        private readonly ILogger<SeoController> _logger;
        private readonly ISeoService _seoService;
        private readonly JsonLdService _jsonLdService;

        public SeoController(ILogger<SeoController> logger, ISeoService seoService, JsonLdService jsonLdService)
        {
            _logger = logger;
            _seoService = seoService;
            _jsonLdService = jsonLdService;
        }

        [HttpGet]
        [Route("HomePage")]
        public IActionResult GetHomePageSeo()
        {
            var title = "Defaul title";
            var description = "Default description";
            var jsonLd = _jsonLdService.HomePage();

            var dto = new DtoSeo
            {
                seoTitle = title,
                seoDescription = description,

                Title = title,
                Description = description,

                JsonLd = jsonLd
            };

            return JsonContent(dto);
        }

        [HttpPost]
        [Route("CoinsPage")]
        public async Task<IActionResult> GetCoinsPageSeo(KeyValuePair<string, string>[] filters)
        {
            var seo = await _seoService.GetSeoByFiltersAsync(filters);
            var jsonLd = _jsonLdService.HomePage();

            var dto = new DtoSeo
            {
                seoTitle = seo.Title,
                seoDescription = seo.Description,

                Title = seo.Title,
                Description = seo.Description,

                JsonLd = jsonLd
            };

            return JsonContent(dto);
        }

        /// <summary>
        /// CoinPageSeo
        /// </summary>
        /// <param name="coinCatalogId">coin Catalog Id</param>
        /// <param name="cityId">City id</param>
        /// <param name="tabName">Prodat / Kupit / null или пусто</param>
        [HttpGet]
        [Route("CoinPage")]
        public async Task<IActionResult> GetCoinPageSeo(int coinCatalogId, int cityId, string? tabName = null)
        {
            DtoSeo seo;

            switch (tabName?.Trim())
            {
                case "buy":
                    seo = await _seoService.GetCoinPageSeoForSellAsync(coinCatalogId, cityId); break;
                case "sell":
                    seo = await _seoService.GetCoinPageSeoForBuyAsync(coinCatalogId, cityId); break;
                default:
                    seo = await _seoService.GetCoinPageSeoForAllAsync(coinCatalogId, cityId); break;
            }

            return JsonContent(seo);
        }

        public ContentResult JsonContent<TObject>(TObject data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            });

            return Content(json, "application/json", Encoding.UTF8);
        }
    }
}
