using EventManager.Core.Domain.Base;

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
        public static Event CreatEvent(Guid id, string name, string description, string location, DateTime startTime, DateTime endTime,User.User user)
        {
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

        private List<Registeration> _registerations= new List<Registeration>();

        public IReadOnlyCollection<Registeration> Registrations
        {
            get
            {
                return _registerations.AsReadOnly();

            }
        }

        public  void RegisterAtEvent(Registeration registration)
        {
            _registerations.Add(registration);
        }

   
    }
}
