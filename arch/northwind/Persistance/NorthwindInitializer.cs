using Domain.Entities;
using Microsoft.EntityFrameworkCore.Internal;

namespace Persistence
{
    public class NorthwindInitializer
    {
        public static void Initialize(NorthwindDbContext context)
        {
            var initializer = new NorthwindInitializer();
            initializer.SeedEverything(context);
        }

        public void SeedEverything(NorthwindDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Customers.Any())
            {
                return;
            }

            SeedCustomers(context);
        }

        private void SeedCustomers(NorthwindDbContext context)
        {
            var customers = new[]
            {
                new Customer {CustomerId = "ALFKI", ContactName = "Maria Anders"}
            };
        }
    }
}
