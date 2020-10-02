using System;
using System.Collections.Generic;
using Subscriptions.SharedKernel;

namespace Subscriptions.Domain
{
    public class Product: Entity
    {
        private Product()
        {
        }
        public Product(string name, decimal amount, BillingPeriod billingPeriod) : this()
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Amount = amount >= 0  ? amount : throw new ArgumentOutOfRangeException(nameof(amount));
            BillingPeriod = billingPeriod;
        }

        public string Name { get; private set;}
        public decimal Amount { get; private set; }
        public BillingPeriod BillingPeriod { get; private set; }
        
        private readonly List<Tag> _tags = new List<Tag>();
        public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

        public void AddTag(Tag tag)
        {
            _tags.Add(tag);
        }
    }
}