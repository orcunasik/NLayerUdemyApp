using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product()
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "Laptop",
                    Stock = 10,
                    Price = 24640,
                    CreatedDate = DateTime.Now

                },
                new Product()
                {
                    Id = 2,
                    CategoryId = 2,
                    Name = "Karartma Geceleri",
                    Stock = 50,
                    Price = 120
                },
                new Product()
                {
                    Id = 3,
                    CategoryId = 1,
                    Name = "Iphone 14Pro",
                    Stock = 100,
                    Price = 47500
                });
        }
    }
}
