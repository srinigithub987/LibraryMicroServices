namespace PaymentService.DTOs;

public class PaymentRequest
{
    public int CustomerId { get; set; }

    public int OrderId { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; }
        = "INR";

    public string PaymentMethod { get; set; }
        = "Card";
}