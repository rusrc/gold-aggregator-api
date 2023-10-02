using GoldAggregator.Parser.DbContext;
using GoldAggregator.Parser.Entities.Entities;

using Microsoft.EntityFrameworkCore;

namespace GoldAggregator.Infrastructure.Repositories.PgSql
{
    public class CitiesRepository : ICitiesRepository
    {
        private readonly ParserDbContext _context;

        public CitiesRepository(ParserDbContext context)
        {
            _context = context;
        }

        public virtual async Task<List<City>> GetCitiesAsync()
        {
            var cities = await _context.Cities.ToListAsync();

            return cities;
        }

        public virtual async Task<City?> GetCityByIdAsync(int cityId)
        {
            // var city = await _context.Cities.FindAsync(cityId);

            if (cityId == 0) return null;

            var cityName = GetCityNameById(cityId);
            var city = new City { Name = cityName };

            return city;
        }

        // TODO 
        // Use only as temp solution for performence
        private string GetCityNameById(int cityId)
        {

            var cities = new Dictionary<int, string>();
            cities.TryAdd(1, "Москве");
            cities.TryAdd(2, "Санкт-Петербурге");
            cities.TryAdd(3, "Нижнем Новгороде");
            cities.TryAdd(4, "Казани");
            cities.TryAdd(5, "Сургуте");
            cities.TryAdd(6, "Севастополе");

            return cities[cityId];
        }
    }
}
