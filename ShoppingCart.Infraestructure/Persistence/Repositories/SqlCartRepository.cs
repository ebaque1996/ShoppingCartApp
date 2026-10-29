using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Interfaces;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Infraestructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Infraestructure.Persistence.Repositories
{
    public sealed class SqlCartRepository : ICartRepository
    {
        private readonly ShoppingCartDbContext _dbContext;

        public SqlCartRepository(ShoppingCartDbContext dbContext)
        {
            _dbContext = dbContext
                ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Cart?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
        }

        public async Task AddAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            await _dbContext.Carts.AddAsync(cart, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            // 1. Ensure the cart itself is tracked as modified
            _dbContext.Carts.Update(cart);

            // 2. Explicitly control the state of each item to avoid EF Core confusion
            foreach (var item in cart.Items)
            {
                // Check if the item is already tracked by the context
                var entry = _dbContext.Entry(item);

                if (entry.State == EntityState.Detached)
                {
                    // If it's completely new to the context, add it explicitly
                    _dbContext.CartItems.Add(item);
                }
                else if (entry.State == EntityState.Modified)
                {
                    // CRITICAL FIX: If EF Core wrongly marked a new item with a pre-filled Guid as Modified,
                    // we can check if it actually exists in the database. 
                    // Alternatively, if we know it's a new item added to the cart, we force it to Added.

                    // A clean way to handle this in a cart is: if the Id is not in the database, treat as Added.
                    bool existsInDb = await _dbContext.CartItems.AnyAsync(i => i.Id == item.Id, cancellationToken);

                    if (!existsInDb)
                    {
                        entry.State = EntityState.Added; // Force Insert for new items
                    }
                    else
                    {
                        entry.State = EntityState.Modified; // Update existing items
                    }
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveItemAsync(CartItem cartItem, CancellationToken cancellationToken = default)
        {
            // Explicitly mark the specific cart item as deleted
            _dbContext.CartItems.Remove(cartItem);

            // Save changes immediately
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            // Remove the cart entity. Due to cascade delete configuration, 
            // all associated CartItems will be automatically deleted as well.
            _dbContext.Carts.Remove(cart);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
