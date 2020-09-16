using System;
using Ardalis.SmartEnum;

namespace Subscriptions.Domain
{
    public abstract class BillingPeriod : SmartEnum<BillingPeriod>
    {
        public static readonly BillingPeriod Weekly = new WeeklyBillingPeriod();
        public static readonly BillingPeriod Monthly = new MonthlyBillingPeriod();

        private BillingPeriod(string name, int value) : base(name, value)
        {
            
        }

        public abstract DateTimeOffset CalculateBillingPeriodEndDate(DateTimeOffset start);

        private sealed class WeeklyBillingPeriod : BillingPeriod
        {
            public WeeklyBillingPeriod() : base("Weekly", 1) {}

            public override DateTimeOffset CalculateBillingPeriodEndDate(DateTimeOffset start) => start.AddDays(7);
        }
        private sealed class MonthlyBillingPeriod : BillingPeriod
        {
            public MonthlyBillingPeriod() : base("Monthly", 2) {}

            public override DateTimeOffset CalculateBillingPeriodEndDate(DateTimeOffset start) => start.AddMonths(1);
        }
    }
}