using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Subscriptions.Controllers;
using Subscriptions.Data;
using Subscriptions.Domain;
using Xunit;

namespace Subscriptions.Tests
{
    public class SubscribeRequestHandlerTests
    {
        [Fact]
        public async Task ShouldAddSubscriptionToTheDatabase()
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
    }
}