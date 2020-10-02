using System;
using Ardalis.SmartEnum;

namespace Subscriptions.Domain
{
    public class BillingPeriod : SmartEnum<BillingPeriod>
    {
        private readonly Func<DateTime> _endDateCalculation;
        public static readonly BillingPeriod Weekly = new BillingPeriod("Weekly", 1, () => DateTime.UtcNow.AddDays(7));
        public static readonly BillingPeriod Monthly = new BillingPeriod("Monthly", 2, () => DateTime.UtcNow.AddMonths(1));

        private BillingPeriod(string name, int value, Func<DateTime> endDateCalculation) : base(name, value)
        {
            _endDateCalculation = endDateCalculation;
        }

        public DateTime CalculateBillingPeriodEndDate()
        {
            return _endDateCalculation();
        }
    }
}