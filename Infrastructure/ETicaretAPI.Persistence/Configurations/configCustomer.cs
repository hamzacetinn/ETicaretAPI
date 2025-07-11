using ETicaretAPI.Persisitence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETicaretAPI.Persistence.Configurations
{
    public class configCustomer : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(builder => builder.Id);
            builder.Property(builder => builder.Id).IsRequired();

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.CreatedData)
                .IsRequired()
                .HasPrecision(0);
            
        }
    }
}
