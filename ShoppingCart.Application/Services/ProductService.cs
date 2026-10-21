using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Application.DTOs.Products;
using ShoppingCart.Application.Interfaces;

namespace ShoppingCart.Application.Services;

public sealed class ProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository
            ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public Task<ProductResponse> CreateAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        return _productRepository.CreateAsync(request, cancellationToken);
    }

    public Task<IReadOnlyCollection<ProductResponse>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return _productRepository.GetAllAsync(cancellationToken);
    }

    public Task<ProductResponse?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return _productRepository.GetByIdAsync(id, cancellationToken);
    }
}