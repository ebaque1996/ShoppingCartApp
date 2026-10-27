using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.DTOs.Carts;
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

        // Método auxiliar privado para futura implementación JWT
        private string GetCurrentUserId()
        {
            // Cuando uses JWT, esto extraerá el ID del usuario autenticado desde el token automáticamente
            // return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException();

            return string.Empty; // Placeholder actual
        }

        // GET: api/carts/{userId}  (Preparado para cambiar a [HttpGet] sin parámetros cuando uses JWT)
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(string userId, CancellationToken cancellationToken)
        {
            // JWT Readiness: En el futuro puedes reemplazar el parámetro 'userId' por:
            // string userId = GetCurrentUserId();

            var cart = await _cartService.GetCartByUserIdAsync(userId, cancellationToken);

            if (cart == null)
            {
                return NotFound(new { message = "No active cart found for this user." });
            }

            return Ok(cart);
        }

        // PATCH: api/carts/{userId}/items
        [HttpPatch("{userId}/items")]
        public async Task<IActionResult> UpdateItemQuantity(string userId, [FromBody] UpdateCartItemRequestDto request, CancellationToken cancellationToken)
        {
            try
            {
                // JWT Readiness: Aquí también podrías omitir el userId de la ruta y usar GetCurrentUserId()
                await _cartService.UpdateItemQuantityAsync(userId, request.ProductId, request.Quantity, cancellationToken);
                return Ok(new { message = "Cart item quantity updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
