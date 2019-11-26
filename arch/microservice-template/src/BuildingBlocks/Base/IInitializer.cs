using System.Threading.Tasks;

namespace Usavc.Common
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}