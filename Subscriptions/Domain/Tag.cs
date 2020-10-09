using System;
using System.Collections.Generic;
using Subscriptions.SharedKernel;

namespace Subscriptions.Domain
{
    public class Tag : Entity
    {
        private Tag()
        {
            
        }
        public Tag(string name): this()
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public string Name { get; private set; }
        private readonly List<Product> _products = new List<Product>();
        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
    }
}