using System.Linq;
using Subscriptions.Domain;
using Subscriptions.Infrastructure;

namespace Subscriptions.Specifications
{
    public class ActiveSubscriptionSpecification : BaseSpecification<Subscription>
    {
        public ActiveSubscriptionSpecification()
        {
            Criteria = s => s.Status == SubscriptionStatus.Active;
        }
    }

    public static class ActiveSubscriptionSpecificationExtension
    {
        public static IQueryable<Subscription> GetCustomerActiveSubscriptions(this IQueryable<Subscription> query)
        {
            return query.Where(new ActiveSubscriptionSpecification());
        }
    }
}