using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Subscriptions.Before.Data;
using Subscriptions.Before.Domain;
using Subscriptions.Before.Services;

namespace Subscriptions.Before.Controllers
{
    public class SubscribeRequest : IRequest
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public string DiscountCode { get; set; }
    }
    public class SubscribeRequestHandler : IRequestHandler<SubscribeRequest>
    {
        private readonly SubscriptionContext _subscriptionContext;
        private readonly IEmailSender _emailSender;

        public SubscribeRequestHandler(SubscriptionContext subscriptionContext,
            IEmailSender emailSender)
        {
            _subscriptionContext = subscriptionContext;
            _emailSender = emailSender;
        }
        public async Task<Unit> Handle(SubscribeRequest request, CancellationToken cancellationToken)
        {
            var customer = await _subscriptionContext
                .Customers
                .Include(x=>x.Subscriptions)
                .FirstAsync(x=> x.Id == request.CustomerId, cancellationToken: cancellationToken);
            
            var product = await _subscriptionContext.Products.FindAsync(request.ProductId);
            
            var subscriptionAmount = product.Amount;
            if (request.DiscountCode == "Cool-20")
            {
                subscriptionAmount *= 0.8M;
            }
            else if (request.DiscountCode == "Awesome-50")
            {
                subscriptionAmount *= 0.5M;
            }

            var currentPeriodEndDate = product.BillingPeriod switch
            {
                BillingPeriod.Weekly => DateTime.UtcNow.AddDays(7),
                BillingPeriod.Monthly => DateTime.UtcNow.AddMonths(1)
            };

            var subscription = new Subscription
            {
                Id = Guid.NewGuid(),
                Customer = customer,
                Product = product,
                Amount = subscriptionAmount,
                Status = SubscriptionStatus.Active,
                CurrentPeriodEndDate = currentPeriodEndDate    
            };
            customer.Subscriptions.Add(subscription);

            await _subscriptionContext.SaveChangesAsync(cancellationToken);
            
            await _emailSender.SendEmailAsync("Congratulations! You subscribed to a cool product");
            return Unit.Value;
        }
    }
}