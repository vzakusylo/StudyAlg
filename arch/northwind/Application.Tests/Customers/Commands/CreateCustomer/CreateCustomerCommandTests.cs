using System.Threading;
using Application.Customer.Commands.CreateCustomer;
using MediatR;
using Moq;
using Xunit;

namespace Application.Tests
{
    public class CreateCustomerCommandTests : CommandTestBase
    {
        [Fact]
        public void Handle_GivenValidRequest_ShouldRaiseCustomerCreationNotification()
        {
            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateCustomerCommand.Handler(_context, mediatorMock.Object);
            var newCustomerId = "QAZQ1";

            var result = sut.Handle(new CreateCustomerCommand() {Id = newCustomerId}, CancellationToken.None);

            mediatorMock.Verify(
                m => m.Publish(It.Is<CustomerCreated>(cc => cc.CustomerId == newCustomerId),
                    It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
