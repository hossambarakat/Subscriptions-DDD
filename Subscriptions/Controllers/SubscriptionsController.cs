using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Subscriptions.Commands;
using Subscriptions.Queries.GetActiveSubscriptions;

namespace Subscriptions.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscriptionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(SubscribeRequest request)
        {
            await _mediator.Send(request);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveSubscriptions(GetActiveSubscriptionsQuery request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

    }
}