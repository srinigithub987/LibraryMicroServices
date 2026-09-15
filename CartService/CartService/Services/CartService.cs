using CartService.Data;
using CartService.DTOs;
using CartService.Models;
using Microsoft.EntityFrameworkCore;

namespace CartService.Services;

public class CartService : ICartService
{
    private readonly CartDbContext _context;

    public CartService(CartDbContext context)
    {
        _context = context;
    }

    public async Task<CartResponse> AddItemAsync(
        AddCartItemRequest request)
    {
        var cart = await _context.Carts
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x =>
                x.CustomerId == request.CustomerId);

        if (cart == null)
        {
            cart = new Cart
            {
                CustomerId = request.CustomerId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Carts.Add(cart);
        }

        var item = cart.Items
            .FirstOrDefault(x =>
                x.ProductId == request.ProductId);

        if (item == null)
        {
            cart.Items.Add(new CartItem
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice
            });
        }
        else
        {
            item.Quantity += request.Quantity;
        }

        await _context.SaveChangesAsync();

        return MapToResponse(cart);
    }

    public async Task<CartResponse?> GetCartAsync(
        int customerId)
    {
        var cart = await _context.Carts
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x =>
                x.CustomerId == customerId);

        if (cart == null)
            return null;
        

        return  MapToResponse(cart);
    }

    public async Task<bool> RemoveItemAsync(
        int customerId,
        int productId)
    {
        var cart = await _context.Carts
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x =>
                x.CustomerId == customerId);

        if (cart == null)
            return false;

        var item = cart.Items
            .FirstOrDefault(x =>
                x.ProductId == productId);

        if (item == null)
            return false;

        _context.CartItems.Remove(item);

        await _context.SaveChangesAsync();

        return true;
    }

    private CartResponse MapToResponse(Cart cart)
{
    return new CartResponse
    {
        Id = cart.Id,
        CustomerId = cart.CustomerId,
        CreatedAt = cart.CreatedAt,

        Items = cart.Items.Select(item => new CartItemResponse
        {
            Id = item.Id,
            ProductId = item.ProductId,
            Quantity = item.Quantity,
            UnitPrice = item.UnitPrice
        }).ToList()
    };
}
}