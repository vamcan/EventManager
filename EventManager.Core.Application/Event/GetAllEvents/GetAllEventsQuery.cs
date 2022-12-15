using EventManager.Core.Application.Base.Common;
using MediatR;

namespace EventManager.Core.Application.Event.GetAllEvents
{
    public class GetAllEventsQuery : IRequest<OperationResult<GetAllEventsResult>>
    {
    }
}
