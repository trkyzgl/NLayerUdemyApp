using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
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


            /*Product ve Category için Seed oluşturduk ama farklılık olması için ProductFeature için burda işlme yapalım*/
            modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature()
            {
                Id = 1,
                Color = "Kırmızı",
                Heigth = 100,
                Width = 200,
                ProductId = 1,
            },
            new ProductFeature()
            {
                Id = 2,
                Color = "mavi",
                Heigth = 100,
                Width = 200,
                ProductId = 2,
            });

            base.OnModelCreating(modelBuilder);
        }


    }
}
