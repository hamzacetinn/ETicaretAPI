using ETicaretAPI.Persisitence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Configurations
{
    public class configOrder : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(model => model.Id); 
            builder.Property(m => m.Id).IsRequired();
            //builder.Property(model => model.Id).IsRequired();

            builder.Property(model => model.CreatedData)
                .IsRequired()
                .HasPrecision(0);
            builder.Property(model => model.UpdatedData)
                .IsRequired()
                .HasPrecision(0);
            builder.HasOne(m => m.Customer)
                .WithMany(m => m.Orders)
                .HasForeignKey(m => m.CustomerId);
                

        }
    }
}
