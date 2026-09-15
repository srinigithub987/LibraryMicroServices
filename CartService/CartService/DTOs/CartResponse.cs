namespace CartService.DTOs;

public class CartResponse
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<CartItemResponse> Items { get; set; } = new();
}

