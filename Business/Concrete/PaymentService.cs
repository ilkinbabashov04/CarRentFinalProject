using Business.Abstract;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PaymentService
    {
        public PaymentIntent CreatePaymentIntent(long amount)
        {
            StripeConfiguration.ApiKey = "sk_test_51RHNMiFNqZP1oskmfBed4YIBRo1ggzs4WuqCvSen40BBczrK903xWHTL9eEaIZBxoOaaDYjKjFWjFriFZvNNakGL00QZ5j3KmE"; // Stripe Secret Key-nizi buraya daxil edin

            var options = new PaymentIntentCreateOptions
            {
                Amount = amount, // Məsələn, 20.00 USD = 2000 cent
                Currency = "usd", // Pul vahidi (USD)
                PaymentMethodTypes = new List<string> { "card" },
            };

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent = service.Create(options);

            return paymentIntent;
        }
    }
}
