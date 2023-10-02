using GoldAggregator.Api.Dto;
using GoldAggregator.Api.Dto.Enums;

namespace GoldAggregator.Infrastructure.Repositories
{
    public interface ICoinPriceRepository
    {
        Task<PagedListDto<CoinCatalogDto>> GetPagedCoinPricesAsync(
            KeyValuePair<string, string>[] filters, 
            int? page = 1, 
            int? pageSize = 10,
            SortCatalogCoins sortBy = SortCatalogCoins.None,
            SortDirection direction = SortDirection.Asc);

        Task<CoinCatalogDto?> GetCoinCatalogWithPricesAsync(int coinCatalogId, int cityId);

        Task<Dictionary<int, DateTime>> GetLastUpdatedCoinCatalogsAsync(int[] coinCatalogIds);
    }
}
