using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Usavc.Services.Appointment.API.Infrastructure.EntityConfigurations;
using Usavc.Services.Appointment.API.Model;

namespace Usavc.Services.Appointment.API.Infrastructure
{


    public class AppointmentContext : DbContext
    {
        public AppointmentContext(DbContextOptions<AppointmentContext> options) : base(options)
        {
        }
        
        public DbSet<ReasonCode> ReasonCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CatalogBrandEntityTypeConfiguration());
        }     
    }


    public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<AppointmentContext>
    {
        public AppointmentContext CreateDbContext(string[] args)
        {
            var optionsBuilder =  new DbContextOptionsBuilder<AppointmentContext>()
                .UseSqlServer("Server=.;Initial Catalog=Usavc.Services.AppointmentDb;Integrated Security=true");

            return new AppointmentContext(optionsBuilder.Options);
        }
    }
}
