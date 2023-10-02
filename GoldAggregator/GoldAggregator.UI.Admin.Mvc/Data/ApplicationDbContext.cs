using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoldAggregator.UI.Admin.Mvc.Data
{
    // TODO Возможно в будущем разделим базу по канону микросервисов
    public class GoldAggregatorAdminDbContext : IdentityDbContext
    {
        public GoldAggregatorAdminDbContext(DbContextOptions<GoldAggregatorAdminDbContext> options)
            : base(options)
        {
        }
    }
}