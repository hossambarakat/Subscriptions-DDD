using Subscriptions.Before.SharedKernel;

namespace Subscriptions.Before.Domain
{
    public class Product: Entity
    {
        public string Name { get; private set;}
        public decimal Amount { get; }
        public BillingPeriod BillingPeriod { get; }
    }

    public enum BillingPeriod
    {
        Weekly,
        Monthly
    }
}