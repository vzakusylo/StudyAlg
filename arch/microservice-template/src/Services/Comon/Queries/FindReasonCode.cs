using System;
using System.Collections.Generic;
using Usavc.Common.Types;
using Usavc.Microservices.Appointment.Dto;

namespace Usavc.Microservices.Appointment.Queries
{
    public class FindReasonCode : IQuery<IEnumerable<ReasonCodeDto>>
    {
        public string AppointmentIdId { get; set; }
    }
}