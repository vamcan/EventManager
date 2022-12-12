﻿using EventManager.Core.Application.Base.Common;
using EventManager.Core.Domain.Contracts.Repository;
using EventManager.Core.Domain.Entities.Event;
using MediatR;

namespace EventManager.Core.Application.Event.RegisterAtEvent
{
    public class RegisterAtEventHandler : IRequestHandler<RegisterAtEventCommand, OperationResult<RegisterAtEventResult>>
    {
        private readonly IEventRepository _eventRepository;


        public RegisterAtEventHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<OperationResult<RegisterAtEventResult>> Handle(RegisterAtEventCommand request, CancellationToken cancellationToken)
        {
            var currentEvent = await _eventRepository.GetEventByIdAsync(request.EventId, cancellationToken);
            var registeration = Registeration.CreateRegisteration(Guid.NewGuid(), request.Name, request.PhoneNumber,
                currentEvent, request.Email);
            currentEvent.RegisterAtEvent(registeration);
            await _eventRepository.UpdateEventAsync(currentEvent, cancellationToken);

            var result = new RegisterAtEventResult()
            {
                Email = request.Email.Value,
                EventEndTime = currentEvent.EndTime,
                EventName = currentEvent.Name,
                EventStartTime = currentEvent.StartTime,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber.Value
            };

            return OperationResult<RegisterAtEventResult>.SuccessResult(result);
        }
    }
}
