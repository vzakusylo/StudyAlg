using Usavc.Common.Types;
using Usavc.Microservices.Appointment.Dto;

namespace Usavc.Microservices.Appointment.Queries
{
    public class GetReasonCode : IQuery<ReasonCodeDto>
    {
        public string Code { get; set; } 
    }
}