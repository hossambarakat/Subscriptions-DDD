using System.Collections.Generic;
using Subscriptions.SharedKernel;

namespace Subscriptions.Domain
{
    public class PricePlan : ValueObject
    {
        private PricePlan()
        {
            
        }
        public PricePlan(decimal amount, BillingPeriod billingPeriod)
        {
            Amount = amount;
            BillingPeriod = billingPeriod;
        }

        public decimal Amount { get; }
        public BillingPeriod BillingPeriod { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return BillingPeriod;
        }
    }
}