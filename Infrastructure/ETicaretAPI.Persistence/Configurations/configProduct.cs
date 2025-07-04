using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ETicaretAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETicaretAPI.Persistence.Configurations
{

    public class configProduct : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(model => model.Id);
            builder.Property(model => model.Id).IsRequired();

            builder.Property(model=> model.Name).IsRequired().HasMaxLength(100);
            builder.Property(model => model.Price).IsRequired().HasPrecision(6,2);
            builder.Property(model => model.Stock).IsRequired();

            
                   
        }
    }
}
