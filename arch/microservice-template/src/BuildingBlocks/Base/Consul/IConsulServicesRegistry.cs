using System.Threading.Tasks;
using Consul;

namespace Usavc.Microservices.Common.Consul
{
    public interface IConsulServicesRegistry
    {
        Task<AgentService> GetAsync(string name);
    }
}