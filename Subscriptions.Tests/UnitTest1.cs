using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Shouldly;
using Subscriptions.Controllers;
using Subscriptions.Data;
using Subscriptions.Domain;
using Subscriptions.Events;
using Xunit;

namespace Subscriptions.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Subscribe_ShouldCreateSubscription()
        {
            var options = new DbContextOptionsBuilder<SubscriptionContext>()
                .UseSqlServer("Server=localhost;Database=Subscriptions;uid=sa;pwd=yourStrong(!)Password;")
                .Options;
            var context = new SubscriptionContext(options, Substitute.For<IMediator>());
            var product = new Product("Flowers", new PricePlan(10, BillingPeriod.Monthly));
            var customer = new Customer(new Email("customer@example.org"),new  FullName("Hossam", "Barakat"));
            await context.Products.AddAsync(product);
            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();
            
            var handler = new SubscribeRequestHandler(context);
            await handler.Handle(new SubscribeRequest()
            {
                CustomerId = customer.Id,
                ProductId = product.Id
            }, CancellationToken.None);

            var subscription = await context.Subscriptions.FirstOrDefaultAsync();
            Assert.NotNull(subscription);
        }
        
        [Fact]
        public async Task Subscribe_ShouldRaiseCustomerSubscribedToProductEvent()
        {
            var options = new DbContextOptionsBuilder<SubscriptionContext>()
                .UseSqlServer("Server=localhost;Database=Subscriptions;uid=sa;pwd=yourStrong(!)Password;")
                .Options;
            var mediator = Substitute.For<IMediator>();
            var context = new SubscriptionContext(options, mediator);
            var product = new Product("Flowers", new PricePlan(10, BillingPeriod.Monthly));
            var customer = new Customer(new Email("customer@example.org"),new  FullName("Hossam", "Barakat"));
            await context.Products.AddAsync(product);
            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();
            
            var handler = new SubscribeRequestHandler(context);
            await handler.Handle(new SubscribeRequest()
            {
                CustomerId = customer.Id,
                ProductId = product.Id
            }, CancellationToken.None);

            await mediator
                .Received()
                .Publish(
                    Arg.Is<CustomerSubscribedToProduct>(e => e.CustomerId == customer.Id && e.ProductId == product.Id), 
                    CancellationToken.None);
        }

        [Fact]
        public void Customer()
        {
            var product = new Product("Flowers", new PricePlan(10, BillingPeriod.Monthly));
            var customer = new Customer(new Email("customer@example.org"),new  FullName("Hossam", "Barakat"));
            customer.SubscribeTo(product, null);
            
            customer.Subscriptions.Count.ShouldBe(1);

            var subscription = customer.Subscriptions.Single();
            subscription.Amount.ShouldBe(10);
        }
        
        [Fact]
        public void Customer_withDiscount()
        {
            var product = new Product("Flowers", new PricePlan(10, BillingPeriod.Monthly));
            var customer = new Customer(new Email("customer@example.org"),new  FullName("Hossam", "Barakat"));
            customer.SubscribeTo(product, "Awesome-50");
            
            customer.Subscriptions.Count.ShouldBe(1);

            var subscription = customer.Subscriptions.Single();
            subscription.Amount.ShouldBe(5);
        }
        
        [Fact]
        public void Customer_raisedEvent()
        {
            var product = new Product("Flowers", new PricePlan(10, BillingPeriod.Monthly));
            var customer = new Customer(new Email("customer@example.org"),new  FullName("Hossam", "Barakat"));
            customer.SubscribeTo(product, "Awesome-50");
            
            customer.Subscriptions.Count.ShouldBe(1);

            customer.DomainEvents.Count.ShouldBe(1);
            var @event = customer.DomainEvents.Single() as CustomerSubscribedToProduct ;
            @event?.CustomerId.ShouldBe(customer.Id);
        }
    }
}