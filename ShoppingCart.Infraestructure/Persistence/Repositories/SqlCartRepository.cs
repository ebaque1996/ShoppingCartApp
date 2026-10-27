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
            _dbContext.Carts.Update(cart);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
