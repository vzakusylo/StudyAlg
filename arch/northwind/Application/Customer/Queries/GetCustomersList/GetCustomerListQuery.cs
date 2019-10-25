using MediatR;

namespace Application.Customer.Queries.GetCustomersList
{
    public class GetCustomerListQuery : IRequest<CustomersListViewModel>
    {
    }
}