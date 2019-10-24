using MediatR;

namespace Application.Tests.Customers.Queues.GetCustomersList
{
    public class GetCustomerListQuery : IRequest<CustomersListViewModel>
    {
    }
}