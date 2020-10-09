using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Shouldly;
using Subscriptions.Commands;
using Subscriptions.Data;
using Subscriptions.Domain;
using Subscriptions.Domain.Services;
using Subscriptions.Infrastructure;
using Xunit;

namespace Subscriptions.Tests
{
    public class SubscribeToProductTests
    {
        [Fact]
        public async Task Should_Add_Subscription_To_Database()
        {
            var options = new DbContextOptionsBuilder<SubscriptionContext>()
                .AddInterceptors(new DomainEventDispatcher(Substitute.For<IMediator>()))
                .UseSqlServer("Server=localhost;Database=Subscriptions;uid=sa;pwd=yourStrong(!)Password;")
                .Options;
            var context = new SubscriptionContext(options);
            await context.Database.EnsureCreatedAsync();
            var product = new Product("Flowers", 10, BillingPeriod.Monthly);
            var customer = new Customer(new Email("customer@example.org"),new  CustomerName("Hossam", "Barakat"));
            await context.Products.AddAsync(product);
            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();

            var handler = new SubscribeRequestHandler(context, new SubscriptionAmountCalculator());
            await handler.Handle(new SubscribeRequest
            {
                CustomerId = customer.Id,
                ProductId = product.Id
            }, CancellationToken.None);

            var subscription = await context.Subscriptions
                .SingleOrDefaultAsync(x=> x.Customer.Id == customer.Id && x.Product.Id == product.Id);
            subscription.ShouldNotBeNull();
        }
    }
}