using System;

namespace Subscriptions.Domain
{
    public class Product
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
        public Guid Id { get; private set; }
        public string Name { get; private set;}
        public PricePlan PricePlan { get; private set;}
    }
}