using GoldAggregator.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GoldAggregator.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private IAnalyticsRepository _analyticsRepository;

        public AnalyticsController(IAnalyticsRepository analyticsRepository)
        {
            _analyticsRepository = analyticsRepository;
        }

        /// <summary>
        /// Динамика цены продажи от дилера
        /// </summary>
        /// <param name="coinCatalogId">id монеты</param>
        /// <param name="lastDays">количество дней начиная с сегодняшнего дня</param>
        [HttpGet("PriceToBuyDynamicRows")]
        public async Task<IActionResult> Get(int coinCatalogId, int lastDays)
        {
            var records = await _analyticsRepository.GetPriceToBuy(coinCatalogId, lastDays);

            return Ok(records);
        }
    }
}
