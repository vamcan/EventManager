using EventManager.Core.Application.Base.Common;
using MediatR;

namespace EventManager.Core.Application.Event.GetEvent
{
    public class GetEventQuery : IRequest<OperationResult<GetEventResult>>
    {
        public Guid EventId { get; set; }
    }
}
