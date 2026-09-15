using Microsoft.AspNetCore.Mvc;
using PaymentService.DTOs;
using PaymentService.Services;

namespace PaymentService.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _service;

    public PaymentController(
        IPaymentService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        PaymentRequest request)
    {
        var payment =
            await _service.CreatePaymentAsync(request);

        return Ok(payment);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(
        int id)
    {
        var payment =
            await _service.GetPaymentAsync(id);

        if (payment == null)
            return NotFound();

        return Ok(payment);
    }
}