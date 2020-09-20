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

        public abstract DateTime CalculateBillingPeriodEndDate(DateTime start);

        private sealed class WeeklyBillingPeriod : BillingPeriod
        {
            public WeeklyBillingPeriod() : base("Weekly", 1) {}

            public override DateTime CalculateBillingPeriodEndDate(DateTime start) => start.AddDays(7);
        }
        private sealed class MonthlyBillingPeriod : BillingPeriod
        {
            public MonthlyBillingPeriod() : base("Monthly", 2) {}

            public override DateTime CalculateBillingPeriodEndDate(DateTime start) => start.AddMonths(1);
        }
    }
}