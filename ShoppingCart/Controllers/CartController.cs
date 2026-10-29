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
            try
            {
                var cartDto = await _cartService.GetCartByUserIdAsync(userId, cancellationToken);

                if (cartDto == null)
                {
                    return NotFound(new { message = "No active cart found for this user." });
                }

                return Ok(cartDto);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // POST: api/carts/{userId}/items
        [HttpPost("{userId}/items")]
        public async Task<IActionResult> AddItemToCart(string userId, [FromBody] AddToCartRequestDto request, CancellationToken cancellationToken)
        {
            try
            {
                // Call service to add product to the cart
                await _cartService.AddToCartAsync(userId, request.ProductId, request.Quantity, cancellationToken);

                // 201 Created (Ideal for successfully created resources)
                return StatusCode(StatusCodes.Status201Created, new { message = "Product added to cart successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                // 404 Not Found (Product not found)
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // 409 Conflict (Insufficient stock)
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                // 400 Bad Request (Invalid quantity)
                return BadRequest(new { message = ex.Message });
            }
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

        // DELETE: api/carts/{userId}/items/{productId}
        [HttpDelete("{userId}/items/{productId}")]
        public async Task<IActionResult> RemoveItemFromCart(string userId, int productId, CancellationToken cancellationToken)
        {
            try
            {
                // Call service to remove the product from the user's cart
                await _cartService.RemoveItemFromCartAsync(userId, productId, cancellationToken);

                // 204 No Content (Standard successful response when a resource is deleted with no body returned)
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // 404 Not Found (Cart or product does not exist)
                return NotFound(new { message = ex.Message });
            }
        }

        // DELETE: api/carts/{userId}
        [HttpDelete("{userId}")]
        public async Task<IActionResult> ClearCart(string userId, CancellationToken cancellationToken)
        {
            try
            {
                // Call service to clear/delete the entire cart
                await _cartService.ClearCartAsync(userId, cancellationToken);

                // 204 No Content (Standard successful response when a resource is deleted)
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                // 404 Not Found (Cart does not exist for this user)
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
