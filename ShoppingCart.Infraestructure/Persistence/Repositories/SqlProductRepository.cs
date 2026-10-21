using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.DTOs.Products;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Infraestructure.Persistence.Contexts;

namespace ShoppingCart.Infraestructure.Persistence.Repositories;

public sealed class SqlProductRepository : IProductRepository
{
    private readonly ShoppingCartDbContext _dbContext;

    public SqlProductRepository(ShoppingCartDbContext dbContext)
    {
        _dbContext = dbContext
            ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<ProductResponse> CreateAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock
        };

        await _dbContext.Products.AddAsync(product, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return MapToResponse(product);
    }

    public async Task<IReadOnlyCollection<ProductResponse>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products
            .AsNoTracking()
            .Select(product => new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<ProductResponse?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products
            .AsNoTracking()
            .Where(product => product.Id == id)
            .Select(product => new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            })
            .SingleOrDefaultAsync(cancellationToken);
    }

    private static ProductResponse MapToResponse(Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock
        };
    }
}
