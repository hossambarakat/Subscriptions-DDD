using System;
using System.Collections.Generic;
using Subscriptions.Domain.Services;
using Subscriptions.Events;
using Subscriptions.SharedKernel;

namespace Subscriptions.Domain
{
    public class Customer: Entity
    {
        private Customer()
        {

        }

        public Customer(Email email, CustomerName customerName): this()
        {
            Id = Guid.NewGuid();
            Email = email ?? throw new ArgumentNullException(nameof(email));
            CustomerName = customerName ?? throw new ArgumentNullException(nameof(customerName));
            _subscriptions = new List<Subscription>();
        }

        public Email Email { get; private set;}
        public CustomerName CustomerName { get; private set;}
        public decimal MoneySpent { get; private set; }
        private readonly List<Subscription> _subscriptions;
        public IReadOnlyCollection<Subscription> Subscriptions => _subscriptions.AsReadOnly();

        public void AddSubscription(Product product, ISubscriptionAmountCalculator subscriptionAmountCalculator)
        {
            var subscriptionAmount = subscriptionAmountCalculator.Calculate(product, this);

            var subscription = new Subscription(this, product, subscriptionAmount);
            _subscriptions.Add(subscription);
            MoneySpent += subscription.Amount;

            AddDomainEvent(new CustomerSubscribedToProduct
            {
                CustomerId = Id,
                ProductId = product.Id
            });
        }
    }
}