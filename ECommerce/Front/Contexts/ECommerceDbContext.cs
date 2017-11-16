using Front.Migrations;
using Front.Models;
using System.Data.Entity;

namespace Front.Contexts
{
    public class ECommerceDbContext :DbContext
    {
        #region Properties

        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Sell> Sells { get; set; }
        public DbSet<SellItem> SellItems { get; set; }

        #endregion Properties

        public ECommerceDbContext()
            : base("Asp_Net_MVC_CS")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ECommerceDbContext, Configuration>());
        }
    }
}