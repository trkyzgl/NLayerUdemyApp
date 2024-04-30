using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Seeds
{
    internal class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            /*Seed işlemi veri tabanına bir ekleme yapılırken manuel olarak bir ekleme yapabilmek için kullanılır. 
             Bunu Migration ekleme yaparken de kullanabiliriz ama biz Seed aracılığıyla yapacağız.
             */
            builder.HasData(
                new Category { Id = 1, Name = "Kalemler" }, 
                new Category { Id = 2, Name = "Kitaplar" },
                new Category { Id = 3, Name = "Defterler" });


            //throw new NotImplementedException();
        }
    }
}
