using System;
using System.Collections.Generic;
using Subscriptions.SharedKernel;

namespace Subscriptions.Domain
{
    public class CustomerName : ValueObject
    {
        public CustomerName(string firstName, string lastName)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }

        public string FirstName { get; }
        public string LastName { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}