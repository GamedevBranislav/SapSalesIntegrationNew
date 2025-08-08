using Microsoft.EntityFrameworkCore;
using SapSalesIntegrationNew.Models;

namespace SapSalesIntegrationNew.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<SalesOrder> SalesOrders { get; set; }
    }
}
