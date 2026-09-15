using PaymentService.Data;
using PaymentService.DTOs;
using PaymentService.Models;
using Microsoft.EntityFrameworkCore;

namespace PaymentService.Services;

public class PaymentService : IPaymentService
{
    private readonly PaymentDbContext _context;

    public PaymentService(
        PaymentDbContext context)
    {
        _context = context;
    }

    public async Task<Payment>
        CreatePaymentAsync(
            PaymentRequest request)
    {
        var payment = new Payment
        {
            CustomerId = request.CustomerId,
            OrderId = request.OrderId,
            Amount = request.Amount,
            Currency = request.Currency,
            PaymentMethod = request.PaymentMethod,
            Status = "Completed",
            TransactionId =
                Guid.NewGuid().ToString(),
            CreatedAt = DateTime.UtcNow,
            CompletedAt = DateTime.UtcNow
        };

        _context.Payments.Add(payment);

        await _context.SaveChangesAsync();

        return payment;
    }

    public async Task<Payment?>
        GetPaymentAsync(int id)
    {
        return await _context.Payments
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}