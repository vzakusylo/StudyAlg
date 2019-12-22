using FluentValidation;
using MediatR;
using Ordering.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private IValidator<TRequest>[] validators;

        public ValidatorBehavior(IValidator<TRequest>[] validators) => this.validators = validators;
      
        public async Task<TResponse> Handle(TRequest request, 
            CancellationToken cancellationToken, 
            RequestHandlerDelegate<TResponse> next)
        {
            var failtures = validators
                 .Select(v => v.Validate(request))
                 .SelectMany(result => result.Errors)
                 .Where(error => error != null)
                 .ToList();

            if (failtures.Any())
            {
                throw new OrderingDomainException($"Command Validation Errors for type  {typeof(TRequest).Name}",
                    new ValidationException("Validation exception"), failtures);
            }

            var response = await next();
            return response;
        }
    }
}
