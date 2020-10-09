using System.Linq;
using Shouldly;
using Subscriptions.Domain;
using Subscriptions.Domain.Services;
using Subscriptions.Events;
using Xunit;

namespace Subscriptions.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void SubscribeToProduct_ShouldAddSubscription()
        {
            var product = new Product("Flowers", 10, BillingPeriod.Monthly);
            var customer = new Customer(new Email("customer@example.org"),new  CustomerName("Hossam", "Barakat"));
            customer.SubscribeTo(product, new SubscriptionAmountCalculator());

            customer.Subscriptions.Count.ShouldBe(1);

            var subscription = customer.Subscriptions.Single();
            subscription.Amount.ShouldBe(10);
        }

        [Fact]
        public void SubscribeToProduct_ShouldRaiseDomainEvent()
        {
            var product = new Product("Flowers", 10, BillingPeriod.Monthly);
            var customer = new Customer(new Email("customer@example.org"),new  CustomerName("Hossam", "Barakat"));
            customer.SubscribeTo(product, new SubscriptionAmountCalculator());

            customer.Subscriptions.Count.ShouldBe(1);

            customer.DomainEvents.Count.ShouldBe(1);
            var @event = customer.DomainEvents.Single() as CustomerSubscribedToProduct ;
            @event?.CustomerId.ShouldBe(customer.Id);
        }
    }
}