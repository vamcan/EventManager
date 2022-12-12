using EventManager.Core.Application.Base.Common;
using EventManager.Core.Application.Event.AddEvent;
using EventManager.Core.Domain.ValueObjects;
using MediatR;

namespace EventManager.Core.Application.Event.RegisterAtEvent
{
    public class RegisterAtEventCommand: IRequest<OperationResult<RegisterAtEventResult>>
    {
        public string Name { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public Email Email { get; set; }
        public Guid EventId { get; set; }
    }
}
