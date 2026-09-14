using PaymentService.DTOs;
using PaymentService.Models;

namespace PaymentService.Services;

public interface IPaymentService
{
    Task<Payment> CreatePaymentAsync(
        PaymentRequest request);

    Task<Payment?> GetPaymentAsync(
        int id);
}