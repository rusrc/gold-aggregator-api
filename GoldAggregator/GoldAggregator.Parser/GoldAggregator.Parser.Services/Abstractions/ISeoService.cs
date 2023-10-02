using GoldAggregator.Api.Dto;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldAggregator.Parser.Services.Abstractions
{
    public interface ISeoService
    {
        Task<DtoSeo> GetSeoByFiltersAsync(KeyValuePair<string, string>[] filters);
        Task<DtoSeo> GetCoinPageSeoForAllAsync(int coinCatalogId, int cityId = 0);
        Task<DtoSeo> GetCoinPageSeoForSellAsync(int coinCatalogId, int cityId = 0);
        Task<DtoSeo> GetCoinPageSeoForBuyAsync(int coinCatalogId, int cityId = 0);      
    }
}
