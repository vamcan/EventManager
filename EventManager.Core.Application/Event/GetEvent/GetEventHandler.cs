using EventManager.Core.Application.Base.Common;
using EventManager.Core.Application.Event.Mapper;
using EventManager.Core.Domain.Contracts.Repository;
using MediatR;

namespace EventManager.Core.Application.Event.GetEvent
{
    public class GetEventHandler : IRequestHandler<GetEventQuery, OperationResult<GetEventResult>>
    {
        private readonly IEventRepository _eventRepository;

        public GetEventHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<OperationResult<GetEventResult>> Handle(GetEventQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var @event = await _eventRepository.GetEventByIdAsync(request.EventId, cancellationToken);
                var result = EventMapper.EventToGetEventResultMapper(@event);
                return OperationResult<GetEventResult>.SuccessResult(result);
            }
            catch (Exception e)
            {
                return OperationResult<GetEventResult>.FailureResult(e.Message);
            }
        }
    }
}
