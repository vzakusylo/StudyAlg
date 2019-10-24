using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Notifications;
using MediatR;

namespace Application.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest
    {
        public string Id { get; set; }

        public string ContactName { get; set; }

        public class Handler : IRequestHandler<CreateCustomerCommand, Unit>
        {
            private readonly INorthwindDbContext _context;
            private readonly IMediator _mediator;

            public Handler(INorthwindDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                var entity = new Domain.Entities.Customer
                {
                    CustomerId = request.Id,
                    ContactName = request.ContactName
                };

                _context.Customers.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                await _mediator.Publish(new CustomerCreated {CustomerId = entity.CustomerId}, cancellationToken);

                return Unit.Value;
            }
        }
    }
}
