using System.Threading.Tasks;
using Usavc.Microservices.Appointment.Domain;
using Usavc.Microservices.Appointment.Repositories;
using Usavc.Microservices.Common.Sql;

namespace Usavc.Services.Discounts.Repositories
{
    public class ReasonCodeRepository : IReasonCodeRepository
    {
        private readonly IRepository<ReasonCode> _repository;

        public ReasonCodeRepository(IRepository<ReasonCode> repository)
        {
            _repository = repository;
        }

        public Task<ReasonCode> GetAsync(string id)
            => _repository.GetAsync(id);

        public Task AddAsync(ReasonCode reasonCode)
            => _repository.AddAsync(reasonCode);
    }
}