using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Application.DTOs.Carts
{
    public class AddToCartRequestDto
    {
        // Required product identifier
        [Required(ErrorMessage = "The ProductId is required.")]
        public int ProductId { get; set; }

        // Quantity to add, must be at least 1
        [Range(1, int.MaxValue, ErrorMessage = "The quantity must be at least 1.")]
        public int Quantity { get; set; }
    }
}
