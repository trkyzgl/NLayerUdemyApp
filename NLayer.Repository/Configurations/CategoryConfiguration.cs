using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);



            // Eğer burda tablo ismi vermezsek default olarak DbContext te ki DbSet teki İsmi alır. ama biz burda manuel olarak yine isimlendirme yapacağız
            builder.ToTable("Categories");
            //throw new NotImplementedException();
        }
    }
}
