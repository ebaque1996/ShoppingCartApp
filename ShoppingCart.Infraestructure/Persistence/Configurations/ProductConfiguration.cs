using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Infraestructure.Persistence.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(product => product.Id);

        builder.Property(product => product.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(product => product.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(product => product.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(product => product.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(product => product.Stock)
            .IsRequired();

        // Seed data for initial products
        builder.HasData(
            new Product { Id = 1, Name = "Mechanical Keyboard", Description = "RGB mechanical keyboard with red switches", Price = 75.50m, Stock = 20 },
            new Product { Id = 2, Name = "Wireless Gaming Mouse", Description = "Ergonomic mouse with high precision sensor", Price = 45.00m, Stock = 35 },
            new Product { Id = 3, Name = "UltraWide Monitor 29\"", Description = "IPS Full HD monitor for productivity and gaming", Price = 299.99m, Stock = 10 },
            new Product { Id = 4, Name = "USB-C Hub 7-in-1", Description = "Multiport adapter with HDMI, USB 3.0, and SD card reader", Price = 29.99m, Stock = 50 },
            new Product { Id = 5, Name = "Noise-Canceling Headphones", Description = "Over-ear wireless headphones with deep bass", Price = 120.00m, Stock = 15 },
            new Product { Id = 6, Name = "Laptop Stand", Description = "Adjustable aluminum stand for laptops up to 17 inches", Price = 34.99m, Stock = 40 },
            new Product { Id = 7, Name = "External SSD 1TB", Description = "High-speed portable solid state drive", Price = 99.99m, Stock = 25 },
            new Product { Id = 8, Name = "WebCam 1080p", Description = "Full HD webcam with built-in microphone for streaming", Price = 49.99m, Stock = 30 },
            new Product { Id = 9, Name = "Ergonomic Office Chair", Description = "Mesh chair with lumbar support and adjustable armrests", Price = 189.99m, Stock = 8 },
            new Product { Id = 10, Name = "Desk LED Lamp", Description = "Dimmable desk lamp with wireless charging base", Price = 39.99m, Stock = 22 },
            new Product { Id = 11, Name = "Bluetooth Speaker", Description = "Waterproof portable speaker with 12-hour battery life", Price = 59.99m, Stock = 28 },
            new Product { Id = 12, Name = "Smartwatch Fitness Tracker", Description = "Heart rate monitor, sleep tracker, and GPS", Price = 89.99m, Stock = 18 },
            new Product { Id = 13, Name = "Wireless Charging Pad", Description = "Fast Qi-certified wireless charger for smartphones", Price = 22.50m, Stock = 60 },
            new Product { Id = 14, Name = "Vertical Mouse", Description = "Ergonomic vertical mouse to prevent wrist strain", Price = 32.99m, Stock = 25 },
            new Product { Id = 15, Name = "MicroSD Card 128GB", Description = "High-speed memory card with adapter", Price = 18.99m, Stock = 75 },
            new Product { Id = 16, Name = "HDMI 2.1 Cable", Description = "4K@120Hz high-speed braided cable 2m", Price = 15.00m, Stock = 100 },
            new Product { Id = 17, Name = "Portable Power Bank 20000mAh", Description = "Fast-charging external battery pack", Price = 45.50m, Stock = 30 },
            new Product { Id = 18, Name = "Desk Mat Extended", Description = "Large waterproof desk pad for keyboard and mouse", Price = 19.99m, Stock = 50 },
            new Product { Id = 19, Name = "Ring Light 10\"", Description = "LED ring light with tripod stand for content creators", Price = 35.00m, Stock = 20 },
            new Product { Id = 20, Name = "Ethernet Cable Cat6", Description = "Snagless patch cable 5 meters", Price = 9.99m, Stock = 120 },
            new Product { Id = 21, Name = "USB Flash Drive 64GB", Description = "High-speed USB 3.0 storage drive", Price = 12.50m, Stock = 90 },
            new Product { Id = 22, Name = "Condenser Microphone", Description = "USB microphone for podcasting, gaming, and recording", Price = 69.99m, Stock = 14 },
            new Product { Id = 23, Name = "Graphics Tablet", Description = "Drawing tablet with stylus for digital art and design", Price = 79.99m, Stock = 12 },
            new Product { Id = 24, Name = "Smart Plug Wi-Fi", Description = "Voice-controlled smart outlet compatible with Alexa and Google", Price = 16.99m, Stock = 45 },
            new Product { Id = 25, Name = "Cable Management Box", Description = "Organize power strips and messy cords cleanly", Price = 14.99m, Stock = 55 }
        );
    }
}
