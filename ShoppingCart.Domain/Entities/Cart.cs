using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; } = string.Empty;

        // Lista estándar de ítems para manipular fácilmente desde el servicio
        public List<CartItem> Items { get; set; } = new();
    }
}
