using EventManager.Core.Application.Base.Common;
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
            var user = await _userRepository.GetUserByIdAsync(request.UserId, cancellationToken);
            var @event = Domain.Entities.Event.Event.CreatEvent(Guid.NewGuid(), request.Name, request.Description,
                request.Location, request.StartTime, request.EndTime,user);
            var newEvent = await _eventRepository.AddEventAsync(@event, cancellationToken);
          
            var result = new AddEventResult()
            {
                Description = newEvent.Description,
                Location = newEvent.Location,
                StartTime = newEvent.StartTime,
                EndTime = newEvent.EndTime,
                Id = newEvent.Id,
                Name = newEvent.Name
            };
                return OperationResult<AddEventResult>.SuccessResult(result);
        }
    }
}
