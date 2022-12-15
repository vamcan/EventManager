using EventManager.Core.Application.Base.Common;
using EventManager.Core.Application.Event.Mapper;
using EventManager.Core.Domain.Contracts.Repository;
using MediatR;

namespace EventManager.Core.Application.Event.GetAllEvents
{
    public class GetAllEventsHandler : IRequestHandler<GetAllEventsQuery, OperationResult<GetAllEventsResult>>
    {
        private readonly IEventRepository _eventRepository;

        public GetAllEventsHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<OperationResult<GetAllEventsResult>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var events = await _eventRepository.GetAllEventsAsync(cancellationToken);

                var result = new GetAllEventsResult()
                {
                    GetEventResults = events.Select(EventMapper.EventToGetEventResultMapper).ToList(),
                };

                return OperationResult<GetAllEventsResult>.SuccessResult(result);

            }
            catch (Exception e)
            {
                return OperationResult<GetAllEventsResult>.FailureResult(e.Message);
            }
        }
    }
}
