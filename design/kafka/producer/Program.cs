using KafkaProducer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace kafka_prj
{
    //https://habr.com/ru/post/543732/

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CreateHostBuilder(args).Build().Run();
            Console.ReadKey();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, collection) =>
            collection.AddHostedService<KafkaProducerService>());
    }
}
