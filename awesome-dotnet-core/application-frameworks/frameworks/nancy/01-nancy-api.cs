using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy;
using Nancy.Owin;
using Nancy.Testing;

namespace nancy_api_01
{
    [TestClass]
    public class Solution
    {
        private Browser _browser;
        [TestMethod]
        public void Main()
        {
            var host = new WebHostBuilder()
                .UseStartup<Startup>()
                .Build();
            host.Run();

            var bootstrapper = new DefaultNancyBootstrapper();
            _browser = new Browser(bootstrapper);
        }
    }

    public class HelloWordModule : NancyModule
    {
        public HelloWordModule()
        {
            Get("v1/hello/{name}", args =>
            {
                string name = args.name;
                var responce = $"Hello {name} !";
                return responce;
            });
        }
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }           
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseOwin(x => x.UseNancy());
        }
    }
}
