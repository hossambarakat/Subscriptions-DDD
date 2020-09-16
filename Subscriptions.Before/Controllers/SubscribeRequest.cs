using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Subscriptions.Before.Data;
using Subscriptions.Before.Domain;
using Subscriptions.Before.Services;

namespace Subscriptions.Before.Controllers
{
    public class SubscribeRequest : IRequest
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public string? DiscountCode { get; set; }
    }
    public class SubscribeRequestHandler : IRequestHandler<SubscribeRequest>
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Subscription> _subscriptionRepository;
        private readonly IEmailSender _emailSender;

        public SubscribeRequestHandler(IRepository<Customer> customerRepository, 
            IRepository<Product> productRepository,
            IRepository<Subscription> subscriptionRepository,
            IEmailSender emailSender)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _subscriptionRepository = subscriptionRepository;
            _emailSender = emailSender;
        }
        public async Task<Unit> Handle(SubscribeRequest request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.FindByIdAsync(request.CustomerId);
            var product = await _productRepository.FindByIdAsync(request.ProductId);
            
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
                BillingPeriod.Weekly => DateTimeOffset.UtcNow.AddDays(7),
                BillingPeriod.Monthly => DateTimeOffset.UtcNow.AddMonths(1)
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
            
            await _subscriptionRepository.Insert(subscription);
            
            customer.Subscriptions.Add(subscription);

            await _customerRepository.SaveChangesAsync();
            
            await _emailSender.SendEmailAsync("Congratulations! You subscribed to a cool product");
            return Unit.Value;
        }
    }
}