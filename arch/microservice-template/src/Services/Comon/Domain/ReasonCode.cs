using Usavc.Microservices.Common.Types;

namespace Usavc.Microservices.Appointment.Domain
{
    public class ReasonCode : IIdentifiable
    {
        public string Id { get; private set; }
        public string Code { get; private set; }
        public string Description { get; private set; }
        public string AppointmentId { get; private set; }

        public ReasonCode(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}