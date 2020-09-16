using System;
using Subscriptions.SharedKernel;

namespace Subscriptions.Events
{
    public class SubscriptionCancelled: IDomainEvent
    {
        public Guid SubscriptionId { get; set; }
    }

    public class SubscriptionCreated : IDomainEvent
    {
        
    }
}