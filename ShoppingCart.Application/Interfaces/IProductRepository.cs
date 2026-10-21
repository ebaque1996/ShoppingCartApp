using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Application.DTOs.Products;

namespace ShoppingCart.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductResponse> CreateAsync(
            CreateProductRequest request,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<ProductResponse>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<ProductResponse?> GetByIdAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}
