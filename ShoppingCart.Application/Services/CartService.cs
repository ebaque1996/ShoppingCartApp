using ShoppingCart.Application.DTOs.Carts;
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

        public async Task<CartResponseDto?> GetCartByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            // 1. Get the domain entity from the repository
            var cart = await _cartRepository.GetByUserIdAsync(userId, cancellationToken);

            if (cart == null)
            {
                return null;
            }

            // 2. Map items and calculate individual total prices
            var itemsDto = cart.Items.Select(i => new CartItemResponseDto
            {
                Id = i.Id,
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity,
                TotalPrice = i.UnitPrice * i.Quantity
            }).ToList();

            // 3. Map Domain Entity to Response DTO and explicitly calculate the TotalAmount
            var responseDto = new CartResponseDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Items = itemsDto,
                TotalAmount = itemsDto.Sum(i => i.TotalPrice) // Explicitly calculated and assigned here
            };

            return responseDto;
        }

        public async Task AddToCartAsync(string userId, int productId, int quantity, CancellationToken cancellationToken = default)
        {
            // Validate that the quantity is greater than zero
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.");
            }

            // 1. Validate that the product exists and check its current stock
            var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            // 2. Get the active cart for the user
            var cart = await _cartRepository.GetByUserIdAsync(userId, cancellationToken);

            if (cart == null)
            {
                // CASE A: The user does not have a cart, create a new one from scratch
                if (product.Stock < quantity)
                {
                    throw new InvalidOperationException($"Insufficient stock. Available stock: {product.Stock}");
                }

                cart = new Cart
                {
                    UserId = userId,
                    Items = new List<CartItem>
                    {
                        new CartItem
                        {
                            ProductId = product.Id,
                            ProductName = product.Name,
                            UnitPrice = product.Price,
                            Quantity = quantity
                        }
                    }
                };

                await _cartRepository.AddAsync(cart, cancellationToken);
            }
            else
            {
                // CASE B: The user already has a cart, check if the product is already inside
                var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

                if (existingItem != null)
                {
                    int totalQuantity = existingItem.Quantity + quantity;

                    if (product.Stock < totalQuantity)
                    {
                        throw new InvalidOperationException($"Insufficient stock. Available stock: {product.Stock}, Current in cart: {existingItem.Quantity}");
                    }

                    existingItem.Quantity = totalQuantity;
                }
                else
                {
                    if (product.Stock < quantity)
                    {
                        throw new InvalidOperationException($"Insufficient stock. Available stock: {product.Stock}");
                    }

                    cart.Items.Add(new CartItem
                    {
                        CartId = cart.Id,
                        ProductId = product.Id,
                        ProductName = product.Name,
                        UnitPrice = product.Price,
                        Quantity = quantity
                    });
                }

                await _cartRepository.UpdateAsync(cart, cancellationToken);
            }
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

        public async Task RemoveItemFromCartAsync(string userId, int productId, CancellationToken cancellationToken = default)
        {
            // 1. Get the active cart for the user
            var cart = await _cartRepository.GetByUserIdAsync(userId, cancellationToken);
            if (cart == null)
            {
                throw new KeyNotFoundException("Cart not found for this user.");
            }

            // 2. Find the item inside the cart items collection
            var itemToRemove = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (itemToRemove == null)
            {
                throw new KeyNotFoundException("Product not found in the cart.");
            }

            // 3. Check if this is the only item remaining in the cart
            if (cart.Items.Count == 1)
            {
                // If it's the last item, delete the entire cart (Cart + CartItem via cascade)
                await _cartRepository.DeleteAsync(cart, cancellationToken);
            }
            else
            {
                // Otherwise, remove just this specific item
                await _cartRepository.RemoveItemAsync(itemToRemove, cancellationToken);
            }
        }

        public async Task ClearCartAsync(string userId, CancellationToken cancellationToken = default)
        {
            // 1. Get the active cart for the user
            var cart = await _cartRepository.GetByUserIdAsync(userId, cancellationToken);
            if (cart == null)
            {
                throw new KeyNotFoundException("Cart not found for this user.");
            }

            // 2. Delete the cart entirely using the repository
            await _cartRepository.DeleteAsync(cart, cancellationToken);
        }
    }
}
