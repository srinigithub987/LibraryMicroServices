using CartService.DTOs;
using CartService.Models;

namespace CartService.Services;

public interface ICartService
{
    Task<Cart> AddItemAsync(
        AddCartItemRequest request);

    Task<Cart?> GetCartAsync(
        int customerId);

    Task<bool> RemoveItemAsync(
        int customerId,
        int productId);
}