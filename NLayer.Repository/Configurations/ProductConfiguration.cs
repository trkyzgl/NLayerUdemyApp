using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(200);

            builder.Property(x => x.Stock).IsRequired();
            // ################.##
            builder.Property(x=>x.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.ToTable("Products");


            /* Best practice olarak CategoryId olarak isimlendirme yapmış olmasaydık Entity de [Key] atribute ü 
             * ile bunun ForeignKey olduğunu belirtmemiz gerekecekti. Ama biz yinede manuel olarak belirtelim */
            // Burada 1-n ilişkinin yapılıyor.
            builder.HasOne(x=>x.Category).WithMany(x => x.Products).HasForeignKey(x=>x.CategoryId);
            
           
            //throw new NotImplementedException();
        }
    }
}
