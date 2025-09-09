using Microsoft.Extensions.Configuration;
using Tazkarti.Service.ServiceInterfaces;
using Stripe;

namespace Tazkarti.Service.Services;

public class StripeService : IStripeService
{
    private readonly IConfiguration configuration;
    
    public StripeService(IConfiguration configuration)
    {
        this.configuration = configuration;
        StripeConfiguration.ApiKey = configuration["Stripe:ApiKey"];
    }

    public PaymentIntent CreatePaymentIntent(long amount, string currency = "usd")
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = amount,
            Currency = currency,
            PaymentMethodTypes = new List<string> { "card" }
        };

        var service = new PaymentIntentService();
        return service.Create(options);
    }
}