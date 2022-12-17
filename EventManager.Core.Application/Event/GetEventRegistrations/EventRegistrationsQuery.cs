using EventManager.Core.Application.Base.Common;
using MediatR;

namespace EventManager.Core.Application.Event.GetEventRegistrations
{
    public class EventRegistrationsQuery : IRequest<OperationResult<GetEventRegistrationsResult>>
    {
        public Guid EventId { get; set; }
    }
}
