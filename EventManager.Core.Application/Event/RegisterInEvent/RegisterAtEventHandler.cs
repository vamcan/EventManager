using EventManager.Core.Application.Base.Common;
using EventManager.Core.Application.Event.AddEvent;
using EventManager.Core.Domain.Contracts.Repository;
using EventManager.Core.Domain.Entities.Event;
using EventManager.Core.Domain.ValueObjects;
using MediatR;

namespace EventManager.Core.Application.Event.RegisterInEvent
{
    public class RegisterInEventHandler : IRequestHandler<RegisterInEventCommand, OperationResult<RegisterInEventResult>>
    {
        private readonly IEventRepository _eventRepository;


        public RegisterInEventHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<OperationResult<RegisterInEventResult>> Handle(RegisterInEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var currentEvent = await _eventRepository.GetEventByIdAsync(request.EventId, cancellationToken);
                if (currentEvent == null)
                {
                    return OperationResult<RegisterInEventResult>.NotFoundResult("Event does not exist");

                }
                var registration = Registration.CreateRegistration(Guid.NewGuid(), request.Name, PhoneNumber.CreateIfNotEmpty(request.PhoneNumber),
                    currentEvent, Email.CreateIfNotEmpty(request.Email));
                currentEvent.RegisterInEvent(registration);
                await _eventRepository.UpdateEventAsync(currentEvent, cancellationToken);

                var result = new RegisterInEventResult()
                {
                    Email = request.Email,
                    EventEndTime = currentEvent.EndTime,
                    EventName = currentEvent.Name,
                    EventStartTime = currentEvent.StartTime,
                    Name = request.Name,
                    PhoneNumber = request.PhoneNumber
                };

                return OperationResult<RegisterInEventResult>.SuccessResult(result);
            }
            catch (Exception e)
            {
                return OperationResult<RegisterInEventResult>.FailureResult(e.Message);
            }

        }
    }
}
