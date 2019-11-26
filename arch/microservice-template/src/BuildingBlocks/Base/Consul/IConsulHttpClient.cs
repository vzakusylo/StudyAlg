using System.Threading.Tasks;

namespace Usavc.Microservices.Common.Consul
{
    public interface IConsulHttpClient
    {
        Task<T> GetAsync<T>(string requestUri);
    }
}

