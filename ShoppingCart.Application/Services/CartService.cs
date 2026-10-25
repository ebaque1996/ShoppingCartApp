using ShoppingCart.Application.Interfaces;
using ShoppingCart.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Application.Services
{
    public sealed class CartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository
                ?? throw new ArgumentNullException(nameof(cartRepository));
        }

        public async Task<Cart?> GetCartByUserIdAsync(string userId)
        {
            return await _cartRepository.GetByUserIdAsync(userId);
        }
    }
}
