namespace PaymentService.Models;

public class Payment
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "INR";
    public string Status { get; set; } = "Pending";
    public string PaymentMethod { get; set; } = "Card";
    public string? TransactionId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}