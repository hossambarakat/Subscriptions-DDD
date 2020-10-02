using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Subscriptions.Data;
using Subscriptions.Domain;
using Xunit;

namespace Subscriptions.Tests
{
    public class ProductTests
    {
        [Fact]
        public async Task AddTag()
        {
            var options = new DbContextOptionsBuilder<SubscriptionContext>()
                .UseSqlServer("Server=localhost;Database=Subscriptions;uid=sa;pwd=yourStrong(!)Password;")
                .Options;
            var context = new SubscriptionContext(options);
            await context.Database.MigrateAsync();
            
            var product = new Product("Weekly Bunch", 1, BillingPeriod.Weekly);
            product.AddTag(new Tag("Rose"));

            context.Add(product);
            await context.SaveChangesAsync();
        }
    }
}