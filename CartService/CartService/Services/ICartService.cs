using CartService.DTOs;
using CartService.Models;

namespace CartService.Services;

public interface ICartService
{
    Task<CartResponse> AddItemAsync(
        AddCartItemRequest request);

    Task<CartResponse?> GetCartAsync(
        int customerId);

    Task<bool> RemoveItemAsync(
        int customerId,
        int productId);
}