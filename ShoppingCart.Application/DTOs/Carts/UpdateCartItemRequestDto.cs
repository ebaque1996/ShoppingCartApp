using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Application.DTOs.Carts
{
    public class UpdateCartItemRequestDto
    {
        [Required(ErrorMessage = "The ProductId is required.")]
        public int ProductId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "The quantity must be greater than or equal to 0.")]
        public int Quantity { get; set; }
    }
}
