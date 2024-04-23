using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }


        // her bir Entity ye karşıık bir DbSet oluşturacağız
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductFeature> productFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*Burada Configuration yapmak yerine , Configuration assmbly lerimizin içinde yaptığımız Configurationları buraya çekebiliriz*/
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }


    }
}
