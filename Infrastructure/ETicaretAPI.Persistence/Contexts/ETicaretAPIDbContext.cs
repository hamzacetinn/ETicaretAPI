using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Persisitence.Contexts
{
    public class ETicaretAPIDbContext : DbContext
    {
        public ETicaretAPIDbContext(DbContextOptions<ETicaretAPIDbContext> options) : base(options)
        {
            //IoC Container da dolduracak
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }



        //SaveChngeAsync Inceptior: 
        //ChangeTracker: Entityler üzerinde yapılan değişikliklerini ya da yeni eklenen verinin yakalanmasını sağlayan propertydir. Update operasyonlarında Tarac edilen verileri yakalayıp elde etmemizi sağlar.
        //.Entries<T>(): T tipindeki entitylerin değişikliklerini takip eder.
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedData = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedData = DateTime.UtcNow,
                    _ => DateTime.UtcNow,
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new configProduct());
            modelBuilder.ApplyConfiguration(new configCustomer());
            modelBuilder.ApplyConfiguration(new configOrder());
        }
    }
}