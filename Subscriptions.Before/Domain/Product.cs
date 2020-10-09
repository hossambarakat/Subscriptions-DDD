using Subscriptions.Before.SharedKernel;

namespace Subscriptions.Before.Domain
{
    public class Product: Entity
    {
        public string Name { get; set;}
        public decimal Amount { get; set; }
        public BillingPeriod BillingPeriod { get; set; }
    }

    public enum BillingPeriod
    {
        Weekly,
        Monthly
    }
}