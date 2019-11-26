using Newtonsoft.Json;
using Usavc.Microservices.Common.Messages;

namespace Usavc.Microservices.Appointment.Messages.Commands
{
    // Immutable
    public class CreateReasonCode : ICommand
    {
        public string Code { get; }
        public string Description { get; set; }

        [JsonConstructor]
        public CreateReasonCode(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }

    public class CreateReasonCodeRejected : IRejectedEvent
    {
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public CreateReasonCodeRejected(string reason, string code)
        {
            Reason = reason;
            Code = code;
        }
    }

    [MessageNamespace("reasoncode")]
    public class ReasonCodeCreated : IEvent
    {
        public string Id { get; }
        public string Code { get; }
        public string Description { get; }

        [JsonConstructor]
        public ReasonCodeCreated(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}