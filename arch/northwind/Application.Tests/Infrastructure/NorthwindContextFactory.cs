using System;
using Microsoft.EntityFrameworkCore;
using Persistence;


namespace Application.Tests.Infrastructure
{
    public class NorthwindContextFactory
    {
        public static NorthwindDbContext Create()
        {
            var options = new DbContextOptionsBuilder<NorthwindDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var context = new NorthwindDbContext(options);

            context.Database.EnsureCreated();

            context.Customers.AddRangeAsync(
                new Domain.Entities.Customer {CustomerId = "ADAM", ContactName = "Adam Cogan"},
                new Domain.Entities.Customer {CustomerId = "JASON", ContactName = "Jason Taylor"},
                new Domain.Entities.Customer(){CustomerId = "BREND", ContactName = "Brendan Richards"});

            context.SaveChanges();

            return context;
        }

        public static void Destroy(NorthwindDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
