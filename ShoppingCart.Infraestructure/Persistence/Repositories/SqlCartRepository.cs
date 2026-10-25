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

        public async Task<Cart?> GetByUserIdAsync(string userId)
        {
            return await _dbContext.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
