using EventManager.Core.Domain.Base;
using EventManager.Core.Domain.Base.Exceptions;

namespace EventManager.Core.Domain.Entities.Event
{
    public class Event : IBaseEntity, IAggregateRoot
    {
        private Event()
        {

        }
        public Guid Id { get; private init; }
        public string Name { get; private init; }
        public string Description { get; private init; }
        public string Location { get; private init; }
        public DateTime StartTime { get; private init; }
        public DateTime EndTime { get; private init; }
        public User.User User { get; private init; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public static Event CreateEvent(Guid id, string name, string description, string location, DateTime startTime, DateTime endTime,User.User user)
        {
            if (user == null)
            {
                throw new DomainStateException("User does not exist");
            }
            if (startTime > endTime)
            {
                throw new DomainStateException("The end time cannot be smaller than the start time");
            }
            if (id == Guid.Empty)
            {
                throw new DomainStateException("Event Id is not valid");
            }
            var model = new Event()
            {
                Id = id,
                Name = name,
                Description = description,
                Location = location,
                StartTime = startTime,
                EndTime = endTime,
                User = user
            };

            return model;
        }

        private List<Registration> _registrations= new List<Registration>();

        public IReadOnlyCollection<Registration> Registrations
        {
            get
            {
                return _registrations.AsReadOnly();

            }
        }

        public  void RegisterAtEvent(Registration registration)
        {
            _registrations.Add(registration);
        }

   
    }
}
