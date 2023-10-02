using GoldAggregator.Api.Dto.Analytics;

namespace GoldAggregator.Infrastructure.Repositories
{
    public interface IAnalyticsRepository
    {
        Task<IEnumerable<DtoDynamicCoinPrice>> GetPriceToBuy(int coinCatalogId, int lastDays = 30);
    }
}