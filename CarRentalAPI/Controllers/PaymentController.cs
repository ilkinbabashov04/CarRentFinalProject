using Business.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // Ödəniş yaratma API-si
        [HttpPost("create-payment-intent")]
        public IActionResult CreatePaymentIntent([FromBody] long amount)
        {
            var paymentIntent = _paymentService.CreatePaymentIntent(amount);
            return Ok(paymentIntent);
        }
    }
}
