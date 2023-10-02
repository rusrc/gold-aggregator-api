using GoldAggregator.Api.Dto.Analytics;
using GoldAggregator.Parser.DbContext;
using Microsoft.Extensions.Logging;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace GoldAggregator.Infrastructure.Repositories.PgSql
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly ILogger<AnalyticsRepository> _logger;
        private readonly ParserDbContext _context;

        public AnalyticsRepository(ILogger<AnalyticsRepository> logger, ParserDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Цена продажи дилера. 
        /// Для пользователя это цена покупки
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<DtoDynamicCoinPrice>> GetPriceToBuy(int coinCatalogId, int lastDays = 30)
        {
            if (lastDays > 30) throw new NotSupportedException("too much days");

            var connection = _context.Database.GetDbConnection();
            var sql = @$"WITH ""Prices"" AS (
                            SELECT TO_CHAR(date_trunc('day', h.""ParseDate""), 'dd.mm.yyyy') ""ParseDate"", MIN(h.""PriceToBuy"") ""MinPriceToBuy"", h.""DealerId"", h.""CoinFromCatalogId""
                                FROM ""CoinPriceHistory"" h
                            GROUP BY date_trunc('day', h.""ParseDate"") , h.""DealerId"", h.""CoinFromCatalogId""
                            HAVING h.""CoinFromCatalogId"" = {coinCatalogId} 
                                AND date_trunc('day', h.""ParseDate"") BETWEEN NOW() -INTERVAL '{lastDays} DAYS' AND NOW()
                            ORDER BY date_trunc('day', h.""ParseDate""), h.""CoinFromCatalogId""
                        )
                        SELECT coin.""Name"" ""CoinCatalogName"", dealer.""Name"" ""DealerName"", price.""ParseDate"", price.""MinPriceToBuy"" 
                            FROM ""Dealers"" dealer
                        INNER JOIN ""Prices"" price on price.""DealerId"" = dealer.""Id""
                        INNER JOIN ""CoinCatalogs"" coin on price.""CoinFromCatalogId"" = coin.""Id""
                            ORDER BY price.""ParseDate"", price.""MinPriceToBuy"";";


            var records = await connection.QueryAsync<DtoDynamicCoinPrice>(sql);

            return records;
        }
    }
}
