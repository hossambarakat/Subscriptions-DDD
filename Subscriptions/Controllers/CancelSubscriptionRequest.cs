using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Subscriptions.Data;

namespace Subscriptions.Controllers
{
    public class CancelSubscriptionRequest: IRequest
    {
        public Guid CustomerId { get; set; }
        public Guid SubscriptionId { get; set; }
    }
    public class CancelSubscriptionRequestHandler: IRequestHandler<CancelSubscriptionRequest>
    {
        private readonly SubscriptionContext _subscriptionContext;

        public CancelSubscriptionRequestHandler(SubscriptionContext subscriptionContext)
        {
            _subscriptionContext = subscriptionContext;
        }
        public async Task<Unit> Handle(CancelSubscriptionRequest request, CancellationToken cancellationToken)
        {
            var customer = await _subscriptionContext.Customers
                .Include(x=>x.Subscriptions)
                .FirstAsync(x=>x.Id==request.CustomerId, cancellationToken: cancellationToken);
            //customer.CancelSubscription()
            return Unit.Value;
        }
    }
}