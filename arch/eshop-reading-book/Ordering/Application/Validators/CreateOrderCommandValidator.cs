using FluentValidation;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Application.Validators
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator(ILogger<CreateOrderCommandValidator> logger)
        {
            RuleFor(command => command.City).NotEmpty();
            RuleFor(command => command.CardNumber).NotEmpty().Length(12, 19);
            RuleFor(command => command.CardExpiration)
                .NotEmpty()
                .Must(BeValidExpirationDate)
                .WithMessage("Please specify a valid card expiration date");
            RuleFor(command => command.OrderItems)
                .Must(ContainOrderItems)
                .WithMessage("No order items found");
        }

        private bool BeValidExpirationDate(DateTime arg)
        {
            return arg > DateTime.UtcNow;
        }

        private bool ContainOrderItems(IEnumerable<OrderItemDto> arg)
        {
           return arg.Any();
        }
    }
}
