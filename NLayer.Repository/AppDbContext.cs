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
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.Entity)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
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
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                            }
                            
                        case EntityState.Modified:
                            {
                                //DB de güncelleme yaparken createddate dokunma, ilk oluşturduğu tarih kalsın
                                Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
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
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ProductFeature>().HasData(
                new ProductFeature()
                {
                    Id = 1,
                    Color = "Kırmızı",
                    Height = 500,
                    Width = 300,
                    ProductId = 1
                },
                new ProductFeature()
                {
                    Id = 2,
                    Color = "Siyah",
                    Height = 60,
                    Width = 40,
                    ProductId = 2
                },
                new ProductFeature()
                {
                    Id = 3,
                    Color = "Mavi",
                    Height = 100,
                    Width = 75,
                    ProductId = 3
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
