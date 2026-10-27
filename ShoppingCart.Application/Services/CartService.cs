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
        private readonly IProductRepository _productRepository; // Necesary for stock validation

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task<Cart?> GetCartByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await _cartRepository.GetByUserIdAsync(userId, cancellationToken);
        }

        public async Task UpdateItemQuantityAsync(string userId, int productId, int newQuantity, CancellationToken cancellationToken = default)
        {
            if (newQuantity < 0)
            {
                throw new ArgumentException("Quantity cannot be negative.");
            }

            var cart = await _cartRepository.GetByUserIdAsync(userId, cancellationToken);
            if (cart == null)
            {
                throw new KeyNotFoundException("No active cart found for this user.");
            }

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
            {
                throw new KeyNotFoundException("Product not found in the cart.");
            }

            var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
            if (product == null)
            {
                throw new InvalidOperationException("Product no longer exists in the system.");
            }

            if (product.Stock < newQuantity)
            {
                throw new InvalidOperationException($"Insufficient stock. Available stock: {product.Stock}");
            }

            if (newQuantity == 0)
            {
                cart.Items.Remove(item);
            }
            else
            {
                item.Quantity = newQuantity;
            }

            await _cartRepository.UpdateAsync(cart, cancellationToken);
        }
    }
}
