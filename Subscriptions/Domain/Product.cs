using System;
using Subscriptions.SharedKernel;

namespace Subscriptions.Domain
{
    public class Product: Entity
    {
        private Product()
        {
            
        }
        public Product(string name, PricePlan pricePlan)
        {
            Id = Guid.NewGuid();
            Name = name;
            PricePlan = pricePlan;
        }
        public string Name { get; private set;}
        public PricePlan PricePlan { get; private set;}
    }
}