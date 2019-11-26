using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Usavc.Services.Appointment.API.Infrastructure
{
    using Microsoft.Extensions.Options;
    using Model;
    using Polly;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;

    public class CatalogContextSeed
    {
        public async Task SeedAsync(AppointmentContext context,IHostingEnvironment env,IOptions<AppointmentSettings> settings,ILogger<CatalogContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(CatalogContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                if (!context.ReasonCodes.Any())
                {
                    await context.ReasonCodes.AddRangeAsync(GetPreconfiguredCatalogBrands());

                    await context.SaveChangesAsync();
                }
            });
        }

        private IEnumerable<ReasonCode> GetPreconfiguredCatalogBrands()
        {
            return new List<ReasonCode>
            {
                new ReasonCode { Code = "Code1", Description = "Code description 1"},
                new ReasonCode { Code = "Code2", Description = "Code description 2"},
                new ReasonCode { Code = "Code3", Description = "Code description 3"},
                new ReasonCode { Code = "Code4", Description = "Code description 4"},
                new ReasonCode { Code = "Other", Description = "Code description 5"}
            };
        }

        private Policy CreatePolicy( ILogger<CatalogContextSeed> logger, string prefix,int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
    }
}
