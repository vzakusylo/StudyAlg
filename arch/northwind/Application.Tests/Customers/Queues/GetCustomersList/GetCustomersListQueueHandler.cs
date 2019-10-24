using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Application.Tests.Customers.Queues.GetCustomersList
{
    public class GetCustomersListQueueHandler : IRequestHandler<GetCustomerListQuery, CustomersListViewModel>
    {
    }

    public class CustomersListViewModel
    {
        public IList<>
    }
}
