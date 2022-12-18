using EventManager.Core.Application.Base.Common;
using MediatR;

namespace EventManager.Core.Application.Event.RegisterInEvent
{
    public class RegisterInEventCommand: IRequest<OperationResult<RegisterInEventResult>>
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Guid EventId { get; set; }
    }
}
