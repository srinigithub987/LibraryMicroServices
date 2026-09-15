using CartService.DTOs;
using CartService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers;

[ApiController]
[Route("api/cart")]
public class CartController : ControllerBase
{
    private readonly ICartService _service;

    public CartController(ICartService service)
    {
        _service = service;
    }

    [HttpPost("items")]
    public async Task<IActionResult> AddItem(
        AddCartItemRequest request)
    {
        var cart = await _service.AddItemAsync(request);

        return Ok(cart);
    }

    [HttpGet("{customerId:int}")]
    public async Task<IActionResult> GetCart(int customerId)
    {
        var cart = await _service.GetCartAsync(customerId);

        if (cart == null)
        {
            return NotFound();
        }

        return Ok(cart);
    }

    [HttpDelete("{customerId:int}/items/{productId:int}")]
    public async Task<IActionResult> RemoveItem(
        int customerId,
        int productId)
    {
        var result = await _service.RemoveItemAsync(
            customerId,
            productId);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}