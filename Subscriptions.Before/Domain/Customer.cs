using System.Collections.Generic;
using Subscriptions.Before.SharedKernel;

namespace Subscriptions.Before.Domain
{
    public class Customer: Entity
    {
        public string Email { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Subscription> Subscriptions = new List<Subscription>();
    }
}