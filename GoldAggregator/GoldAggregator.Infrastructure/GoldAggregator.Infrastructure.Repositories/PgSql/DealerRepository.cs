using GoldAggregator.Parser.DbContext;

using Microsoft.EntityFrameworkCore;

namespace GoldAggregator.Infrastructure.Repositories.PgSql
{
    public class DealerRepository : IDealerRepository
    {
        private readonly ParserDbContext _context;

        public DealerRepository(ParserDbContext context)
        {
            _context = context;
        }

        public virtual async Task<int[]?> GetDealerIdsAsync(int cityId)
        {
            var filteredDealersByCityId = _context.Dealers.Include(d => d.Offices).Where(d => d.HasDelivery == true || d.Offices.Any(o => o.CityId == cityId));
            var dealersId = await filteredDealersByCityId.Select(d => d.Id).ToArrayAsync();

            return dealersId;
        }
    }
}
