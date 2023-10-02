using GoldAggregator.Api.Dto;

namespace GoldAggregator.Infrastructure.Repositories
{
    public interface IFiltersRepository
    {
        Task<KeyLabelValuePair[]> GetFiltersAsync();
    }
}
