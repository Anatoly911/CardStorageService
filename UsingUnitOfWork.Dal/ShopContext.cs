using System.Data.Entity.Core.Objects;
using System.Data.Entity;
using Unit_of_Work;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Infrastructure;

namespace UsingUnitOfWork.Dal
{
    public class ShopContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Product> Products { get; set; }
        static ShopContext()
        {
            Database.SetInitializer<ShopContext>(new
            DropCreateDatabaseAlways<ShopContext>());
        }
        public ObjectResult<T> ExecuteStoreQuery<T>(string commandText, params
        object[] paramenters)
        {
            return ObjectContext.ExecuteStoreQuery<T>(commandText, paramenters);
        }
        public ObjectContext ObjectContext
        {
            get { return ((IObjectContextAdapter)this).ObjectContext; }
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(GetCustomerConfig());
            modelBuilder.Configurations.Add(GetProductConfig());
            modelBuilder.Configurations.Add(GetSaleItemConfig());
            modelBuilder.Configurations.Add(GetSaleConfig());
        }
        private static EntityTypeConfiguration<Customer> GetCustomerConfig()
        {
            var config = new EntityTypeConfiguration<Customer>();
            config.Property(e => e.Age);
            config.Property(e => e.FirstName).HasMaxLength(256).IsRequired();
            config.Property(e => e.LastName).HasMaxLength(256);
            return config;
        }
        private static EntityTypeConfiguration<Product> GetProductConfig()
        {
            var config = new EntityTypeConfiguration<Product>();
            config.Property(e => e.Description).HasMaxLength(4000);
            config.Property(e => e.Price);
            config.Property(e => e.Name).HasMaxLength(256);
            return config;
        }
        private static EntityTypeConfiguration<SaleItem> GetSaleItemConfig()
        {
            var config = new EntityTypeConfiguration<SaleItem>();
            config.Property(e => e.Comment).HasMaxLength(500);
            return config;
        }
        private static EntityTypeConfiguration<Sale> GetSaleConfig()
        {
            var config = new EntityTypeConfiguration<Sale>();
            config.Property(e => e.Discount);
            config.Property(e => e.TotalCost);
            return config;
        }
    }
}
