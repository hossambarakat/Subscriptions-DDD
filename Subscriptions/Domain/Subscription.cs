using System;
using Subscriptions.Events;
using Subscriptions.SharedKernel;

namespace Subscriptions.Domain
{
    public class Subscription : Entity
    {
        private Subscription()
        {
            
        }
        public Subscription(Customer customer, Product product, decimal amount)
        {
            Id = Guid.NewGuid();
            Customer = customer;
            Product = product;
            Amount = amount;
            Status = SubscriptionStatus.Active;
            CurrentPeriodEndDate = product.PricePlan.BillingPeriod.CalculateBillingPeriodEndDate(DateTimeOffset.UtcNow);
        }
        public Guid Id { get; }
        public SubscriptionStatus Status { get; private set; }
        public Customer Customer { get; private set; }
        public Product Product { get; private set; }
        public decimal Amount { get; private set; }
        public DateTimeOffset CurrentPeriodEndDate { get; private set; } 

        public void CancelSubscription()
        {
            Status = SubscriptionStatus.Cancelled;
            
            AddDomainEvent(new SubscriptionCancelled
            {
                SubscriptionId = Id
            });
        }
    }
}