using System;
using System.Collections.Generic;
using Subscriptions.Events;
using Subscriptions.SharedKernel;

namespace Subscriptions.Domain
{
    public class Customer: Entity
    {
        private Customer(): base()
        {
            
        }

        public Customer(Email email, FullName fullName)
        {
            Id = Guid.NewGuid();
            Email = email;
            FullName = fullName;
            _subscriptions = new List<Subscription>();
        }

        public Email Email { get; private set;}
        public FullName FullName { get; private set;}
        private readonly List<Subscription> _subscriptions = new List<Subscription>();
        public IReadOnlyCollection<Subscription> Subscriptions => _subscriptions.AsReadOnly();

        public Subscription SubscribeTo(Product product, string? discountCode)
        {
            var subscriptionAmount = product.PricePlan.Amount;
            if (discountCode == "Cool-20")
            {
                subscriptionAmount *= 0.8M;
            }
            else if (discountCode == "Awesome-50")
            {
                subscriptionAmount *= 0.5M;
            }

            var subscription = new Subscription(this, product, subscriptionAmount);
            _subscriptions.Add(subscription);
            
            AddDomainEvent(new CustomerSubscribedToProduct()
            {
                CustomerId = Id,
                ProductId = product.Id
            });

            return subscription;
        }
    }
}