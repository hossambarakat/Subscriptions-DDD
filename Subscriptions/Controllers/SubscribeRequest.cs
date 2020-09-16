using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Subscriptions.Data;

namespace Subscriptions.Controllers
{
    public class SubscribeRequest : IRequest
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public string? DiscountCode { get; set; }
    }
    public class SubscribeRequestHandler : IRequestHandler<SubscribeRequest>
    {
        private readonly SubscriptionContext _subscriptionContext;
        public SubscribeRequestHandler(SubscriptionContext subscriptionContext)
        {
            _subscriptionContext = subscriptionContext;
        }
        public async Task<Unit> Handle(SubscribeRequest request, CancellationToken cancellationToken)
        {
            var customer = await _subscriptionContext.Customers.FindAsync(request.CustomerId);
            var product = await _subscriptionContext.Products.FindAsync(request.ProductId);
            
            var subscription = customer.SubscribeTo(product, request.DiscountCode);

            await _subscriptionContext.Subscriptions.AddAsync(subscription, cancellationToken);

            await _subscriptionContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}