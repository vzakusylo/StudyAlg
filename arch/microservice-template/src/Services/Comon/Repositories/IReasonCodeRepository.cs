using System.Threading.Tasks;
using Usavc.Microservices.Appointment.Domain;

namespace Usavc.Microservices.Appointment.Repositories
{
    public interface IReasonCodeRepository
    {
        Task<ReasonCode> GetAsync(string code);
        Task AddAsync(ReasonCode reasonCode);
    }
}