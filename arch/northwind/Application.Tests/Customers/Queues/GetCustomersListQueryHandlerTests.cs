using System.Threading;
using System.Threading.Tasks;
using Application.Customer.Queries.GetCustomersList;
using Application.Tests.Infrastructure;
using AutoMapper;
using Persistence;
using Shouldly;
using Xunit;

namespace Application.Tests.Customers.Queues
{
    public class GetCustomersListQueryHandlerTests
    {
        private readonly NorthwindDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomersListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetCustomersTest()
        {
            var sut = new GetCustomersListQueueHandler(_context, _mapper);
            var result = await sut.Handle(new GetCustomerListQuery(), CancellationToken.None);

            result.ShouldBeOfType<CustomersListViewModel>();

            result.Customers.Count.ShouldBe(3);
        }
    }
}
