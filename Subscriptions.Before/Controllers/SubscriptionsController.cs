using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Subscriptions.Before.Commands;

namespace Subscriptions.Before.Controllers
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
    }
}