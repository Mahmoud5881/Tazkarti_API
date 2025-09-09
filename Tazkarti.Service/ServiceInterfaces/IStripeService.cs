using Stripe;

namespace Tazkarti.Service.ServiceInterfaces;

public interface IStripeService
{
    PaymentIntent CreatePaymentIntent(long amount, string currency = "usd");
}