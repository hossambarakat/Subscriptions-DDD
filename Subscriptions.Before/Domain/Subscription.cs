using System;
using Subscriptions.Before.SharedKernel;

namespace Subscriptions.Before.Domain
{
    public class Subscription : Entity
    {
        public SubscriptionStatus Status { get; set; }
        public Customer Customer { get; set; }
        public Product Product { get; set; }
        public decimal Amount { get; set; }
        public DateTime CurrentPeriodEndDate { get; set; }
    }
}