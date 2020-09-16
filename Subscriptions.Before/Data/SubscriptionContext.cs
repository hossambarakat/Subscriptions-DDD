using Microsoft.EntityFrameworkCore;
using Subscriptions.Before.Domain;

namespace Subscriptions.Before.Data
{
    public class SubscriptionContext: DbContext
    {
        public SubscriptionContext(DbContextOptions<SubscriptionContext> options)
            : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SubscriptionContext).Assembly);
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
    }
}