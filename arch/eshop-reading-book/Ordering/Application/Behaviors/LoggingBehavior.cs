using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.IntegrationEvents.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TRequest>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest,TResponse>> logger)
        {
            this.logger = logger;
        }
        
        public async Task<TRequest> Handle(TRequest request, CancellationToken cancellationToken, 
            RequestHandlerDelegate<TRequest> next)
        {
            logger.LogInformation("----- Handling command {CommandName} ({@Command})", 
                request.GetGenericTypeName(), request);
           var response = await next();
            logger.LogInformation("------ Command {CommandName} handled - response : {@Response}", 
                request.GetGenericTypeName(), 
                response);

            return response;
        }
    }
}
