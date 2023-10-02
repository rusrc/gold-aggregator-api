using GoldAggregator.Infrastructure.Repositories.PgSql;
using GoldAggregator.Parser.DbContext;

namespace GoldAggregator.Infrastructure.Repositories.Cache
{
    public class CachedDealerRepository : DealerRepository
    {
        public CachedDealerRepository(ParserDbContext context) : base(context)
        {
        }

        public override Task<int[]?> GetDealerIdsAsync(int cityId)
        {
            // TODO добавить кеш дилерам на 1 час не меньше
            return base.GetDealerIdsAsync(cityId);
        }
    }
}
