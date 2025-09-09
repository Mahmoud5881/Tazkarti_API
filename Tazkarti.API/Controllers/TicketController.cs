using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.V2.Core;
using Tazkarti.Service.ServiceInterfaces;

namespace Tazkarti.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IEventService eventService;
        private readonly IStripeService stripeService;

        public TicketController(IEventService eventService, IStripeService stripeService)
        {
            this.eventService = eventService;
            this.stripeService = stripeService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetTicketForEvent(int EventId)
        {
            var Event = await eventService.GetEventWithTicketsAsync(EventId);
            if (Event == null)
            {
                return NotFound("Event not found");
            }
            if (Event.Tickets.Count == 0)
            {
                return BadRequest("No tickets for this event");
            }

            var paymentIntent = stripeService.CreatePaymentIntent((long)Event.Price * 100, "usd");
            return Ok(new
            {
                clientSecret = paymentIntent.ClientSecret
            });
        }
    }
}
