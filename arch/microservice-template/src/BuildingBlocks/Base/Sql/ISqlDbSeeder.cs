using System.Threading.Tasks;

namespace Usavc.Microservices.Common.Sql
{
    public interface ISqlDbSeeder
    {
        Task SeedAsync();
    }
}