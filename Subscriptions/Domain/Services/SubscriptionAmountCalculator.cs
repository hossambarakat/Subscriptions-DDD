namespace Subscriptions.Domain.Services
{
    public interface ISubscriptionAmountCalculator
    {
        decimal Calculate(Product product, Customer customer);
    }
    public class SubscriptionAmountCalculator: ISubscriptionAmountCalculator
    {
        public decimal Calculate(Product product, Customer customer)
        {
            var subscriptionAmount = product.Amount;
            if (customer.MoneySpent >= 100)
            {
                subscriptionAmount *= 0.8M;
            }
            else if (customer.MoneySpent >= 1000)
            {
                subscriptionAmount *= 0.5M;
            }

            return subscriptionAmount;
        }
    }
}