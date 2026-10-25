using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Services;

namespace ShoppingCart.Api.Controllers
{
    [ApiController]
    [Route("api/carts")]
    public sealed class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        // GET: api/carts/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                return NotFound(new { message = "No se encontró un carrito activo para este usuario." });
            }

            return Ok(cart);
        }
    }
}
