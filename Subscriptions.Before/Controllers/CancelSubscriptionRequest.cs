using System;
using MediatR;

namespace Subscriptions.Before.Controllers
{
    public class CancelSubscriptionRequest: IRequest
    {
        public Guid SubscriptionId { get; set; }
    }
}