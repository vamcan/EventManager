using EventManager.Core.Application.Base.Common;
using EventManager.Core.Application.Event.Mapper;
using EventManager.Core.Domain.Contracts.Repository;
using MediatR;

namespace EventManager.Core.Application.Event.GetEventRegistrations
{
    public class GetEventRegistrationsHandler : IRequestHandler<EventRegistrationsQuery, OperationResult<GetEventRegistrationsResult>>
    {
        private readonly IEventRepository _eventRepository;

        public GetEventRegistrationsHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<OperationResult<GetEventRegistrationsResult>> Handle(EventRegistrationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var @event = await _eventRepository.GetEventByIdAsync(request.EventId, cancellationToken);
                if (@event == null)
                {
                    return OperationResult<GetEventRegistrationsResult>.NotFoundResult("Event does not exist");
                }

                var eventRegistrations = await _eventRepository.GetEventRegistrationsAsync(@event, cancellationToken);

                var result = new GetEventRegistrationsResult()
                {
                    EventName = @event.Name,
                    EventRegistrations = eventRegistrations.Select(RegistrationMapper.RegistrationToEventRegistrationDtoMapper).ToList(),
                };

                return OperationResult<GetEventRegistrationsResult>.SuccessResult(result);

            }
            catch (Exception e)
            {
                return OperationResult<GetEventRegistrationsResult>.FailureResult(e.Message);
            }
        }
    }
}
