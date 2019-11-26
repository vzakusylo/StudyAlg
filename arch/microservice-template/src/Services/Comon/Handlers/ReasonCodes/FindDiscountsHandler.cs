using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Usavc.Common.Handlers;
using Usavc.Microservices.Appointment.Domain;
using Usavc.Microservices.Appointment.Dto;
using Usavc.Microservices.Appointment.Metrics;
using Usavc.Microservices.Appointment.Queries;
using Usavc.Microservices.Common.Sql;

namespace Usavc.Services.Discounts.Handlers.Discounts
{
    public class FindDiscountsHandler : IQueryHandler<FindReasonCode, IEnumerable<ReasonCodeDto>>
    {
        private readonly IRepository<ReasonCode> _reasonCodeRepository;
        private readonly IMetricsRegistry _registry;    

        public FindDiscountsHandler(IRepository<ReasonCode>  reasonCodeRepository, IMetricsRegistry registry)
        {
            _reasonCodeRepository = reasonCodeRepository;
            _registry = registry;
        }

        public async Task<IEnumerable<ReasonCodeDto>> HandleAsync(FindReasonCode query)
        {
            _registry.IncrementFindDiscountsQuery();
            
            var codes = await _reasonCodeRepository.FindAsync(
                c => c.AppointmentId == query.AppointmentIdId);

            return codes.Select(rc => new ReasonCodeDto
            {
                Code = rc.Code,
                Description = rc.Description
            });
        }
    }
}