using GoldAggregator.Parser.Entities.Entities;

namespace GoldAggregator.Infrastructure.Repositories
{
    public interface ICitiesRepository
    {
        Task<List<City>> GetCitiesAsync();
        Task<City?> GetCityByIdAsync(int cityId);
    }
}
