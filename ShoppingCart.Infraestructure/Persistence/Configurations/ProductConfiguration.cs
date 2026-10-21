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
    }
}
