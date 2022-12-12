﻿using EventManager.Core.Application.Base.Common;
using EventManager.Core.Domain.Contracts.Repository;
using MediatR;

namespace EventManager.Core.Application.Event.AddEvent
{
    public class AddEventHandler : IRequestHandler<AddEventCommand, OperationResult<AddEventResult>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;

        public AddEventHandler(IEventRepository eventRepository, IUserRepository userRepository)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }

        public async Task<OperationResult<AddEventResult>> Handle(AddEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(request.UserId, cancellationToken);
                if (user == null)
                {
                    return OperationResult<AddEventResult>.NotFoundResult("User does not exist");
                }
                var @event = Domain.Entities.Event.Event.CreatEvent(Guid.NewGuid(), request.Name, request.Description,
                    request.Location, request.StartTime, request.EndTime, user);
                var newEvent = await _eventRepository.AddEventAsync(@event, cancellationToken);

                var result = new AddEventResult()
                {
                    Id = newEvent.Id,
                    Name = newEvent.Name,
                    Description = newEvent.Description,
                    Location = newEvent.Location,
                    StartTime = newEvent.StartTime,
                    EndTime = newEvent.EndTime,
                };
                return OperationResult<AddEventResult>.SuccessResult(result);
            }
            catch (Exception e)
            {
                return OperationResult<AddEventResult>.FailureResult(e.Message);
            }
        }
    }
}
