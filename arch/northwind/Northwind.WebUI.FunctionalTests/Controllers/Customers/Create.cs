using System.Net.Http;
using System.Threading.Tasks;
using Application.Customer.Commands.CreateCustomer;
using Northwind.WebUI.FunctionalTests.Common;
using Xunit;

namespace Northwind.WebUI.FunctionalTests.Controllers.Customers
{
    public class Create : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public Create(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GivenCreateCustomerCommand_ReturnsSuccessStatusCode()
        {
            var command = new CreateCustomerCommand
            {
                Id = "ABCDE",
                ContactName = "Vadym Zakusylo"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await _client.PostAsync("/api/customer/create", content);

            response.EnsureSuccessStatusCode();
        }
    }
}
