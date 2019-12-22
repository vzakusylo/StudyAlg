using System;

namespace Ordering.Domain.Exceptions
{
    public class OrderingDomainException : Exception
    {
        public OrderingDomainException(string message) : base(message)
        {
        }

        public OrderingDomainException(string message, FluentValidation.ValidationException validationException, System.Collections.Generic.List<FluentValidation.Results.ValidationFailure> failtures) : base(message)
        {
        }
    }
}
