using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Subscriptions.Data;
using Subscriptions.Specifications;

namespace Subscriptions.Queries.GetActiveSubscriptions
{
    public class GetActiveSubscriptionsQuery : IRequest<List<GetActiveSubscriptionsResponse>>
    {
        public Guid CustomerId { get; set; }
    }

    public class GetActiveSubscriptionsResponse
    {
        public string ProductName { get; set; }
        public string BillingPeriod { get; set; }
    }

    public class GetActiveSubscriptionsQueryHandler
        : IRequestHandler<GetActiveSubscriptionsQuery, List<GetActiveSubscriptionsResponse>>
    {
        private readonly SubscriptionContext _context;

        public GetActiveSubscriptionsQueryHandler(SubscriptionContext context)
        {
            _context = context;
        }
        public async Task<List<GetActiveSubscriptionsResponse>> Handle(GetActiveSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            var queryResult = await _context.Subscriptions
                .GetCustomerActiveSubscriptions()
                .ForCustomer(request.CustomerId)
                .Select(x=> new GetActiveSubscriptionsResponse
                {
                    ProductName = x.Product.Name,
                    BillingPeriod = x.Product.BillingPeriod.ToString()
                })
                .ToListAsync(cancellationToken);
            return queryResult;
        }
    }
}