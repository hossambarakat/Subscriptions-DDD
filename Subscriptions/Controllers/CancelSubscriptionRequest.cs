using System;
using MediatR;

namespace Subscriptions.Controllers
{
    public class CancelSubscriptionRequest: IRequest
    {
        public Guid SubscriptionId { get; set; }
    }
}