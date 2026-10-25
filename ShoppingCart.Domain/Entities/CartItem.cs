using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;

        // Relación con el carrito
        public Guid CartId { get; set; }
        public Cart Cart { get; set; } = null!;
    }
}
