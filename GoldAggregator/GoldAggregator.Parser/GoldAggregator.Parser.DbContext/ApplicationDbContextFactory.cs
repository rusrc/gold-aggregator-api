using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GoldAggregator.Parser.DbContext
{
    /// <summary>
    /// Эта фабрика нужна для автоматического скафолдинга админки.
    /// Также для инстраукции "Update-Database -StartupProject GoldAggregator.Api\GoldAggregator.Api -Project GoldAggregator.Parser\GoldAggregator.Parser.DbContext -Verbose"
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ParserDbContext>
    {
        const string defaultConnection = "Host=188.134.85.64;Port=5432;Database=ParserDbContext;Username=postgres;Password=******;Maximum Pool Size=100";
        const string localConnection = "Host=localhost;Port=5432;Database=ParserDbContext;Username=postgres;Password=******;Maximum Pool Size=100";
        public ParserDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ParserDbContext>();
            optionsBuilder.UseNpgsql(defaultConnection);

            return new ParserDbContext(optionsBuilder.Options);
        }
    }
}
