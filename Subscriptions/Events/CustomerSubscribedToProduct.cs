using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Subscriptions.Services;
using Subscriptions.SharedKernel;

namespace Subscriptions.Events
{
    public class CustomerSubscribedToProduct : IDomainEvent
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
    }
    public class CustomerSubscribedToProductHandler : INotificationHandler<CustomerSubscribedToProduct>
    {
        private readonly IEmailSender _emailSender;

        public CustomerSubscribedToProductHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public Task Handle(CustomerSubscribedToProduct notification, CancellationToken cancellationToken)
        {
            _emailSender.SendEmailAsync("Congratulations! You subscribed to a cool product");
            return Task.CompletedTask;
        }
    }
}