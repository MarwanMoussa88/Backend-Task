using Backend_Task.Entities;
using Backend_Task.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend_Task.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
             new Product
             { Category = "Diary", DiscountRate = 5, ProductCode = "P001", MinQuantity = 10, Name = "Milk", Price = 10 },
             new Product
             { Category = "Poultary", DiscountRate = 10, ProductCode = "P002", MinQuantity = 20, Name = "Chicken", Price = 20 },
             new Product
             { Category = "Vegtables", DiscountRate = 15, ProductCode = "P003", MinQuantity = 20, Name = "Tomato", Price = 5 });
        }
    }
}
