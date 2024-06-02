using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using System.Reflection;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // her bir Entity ye karşıık bir DbSet oluşturacağız
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> productFeatures { get; set; }

        public override int SaveChanges()
        {            // bu method bir entity geldiğinde eğer yeni ekleniyorsa createdDate= Datetime.Now() olacak, eğer  Modified ediliyorsa UpdateDate = DateTime.Now; olacak şekilde ayarlandı
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreateDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x => x.CreateDate).IsModified = false;
                                entityReference.UpdateDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // bu async method bir entity geldiğinde eğer yeni ekleniyorsa createdDate= Datetime.Now() olacak, eğer  Modified ediliyorsa UpdateDate = DateTime.Now; olacak şekilde ayarlandı
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreateDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x => x.CreateDate).IsModified = false;
                                entityReference.UpdateDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*Burada Configuration yapmak yerine , Configuration assmbly lerimizin içinde yaptığımız Configurationları buraya çekebiliriz*/
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            /*Product ve Category için Seed oluşturduk ama farklılık olması için ProductFeature için burda işlem yapalım*/
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
