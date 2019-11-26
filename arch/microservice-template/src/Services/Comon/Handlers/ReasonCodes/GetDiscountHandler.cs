using System.Threading.Tasks;
using Usavc.Common.Handlers;
using Usavc.Microservices.Appointment.Domain;
using Usavc.Microservices.Appointment.Dto;
using Usavc.Microservices.Appointment.Queries;
using Usavc.Microservices.Common.Sql;

namespace Usavc.Services.Discounts.Handlers.Discounts
{
    public class GetDiscountHandler : IQueryHandler<GetReasonCode, ReasonCodeDto>
    {
        private readonly IRepository<ReasonCode> _reasonCodeRepository;

        public GetDiscountHandler(IRepository<ReasonCode> reasonCodeRepository)
        {
            _reasonCodeRepository = reasonCodeRepository;
        }

        public async Task<ReasonCodeDto> HandleAsync(GetReasonCode query)
        {
            var reasonCode = await _reasonCodeRepository.GetAsync(query.Code);
            if (reasonCode is null)
            {
                return null;
            }

            return new ReasonCodeDto
            {
                Code = reasonCode.Code,
                Description = reasonCode.Description
            };
        }
    }
}