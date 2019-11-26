using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Usavc.Common.Logging;
using Usavc.Common.Metrics;

namespace Usavc.Microservices.Appointment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseLogging()
                .UseAppMetrics();
    }
}
