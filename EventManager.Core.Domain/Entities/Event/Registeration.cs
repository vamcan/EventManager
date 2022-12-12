using EventManager.Core.Domain.ValueObjects;

namespace EventManager.Core.Domain.Entities.Event
{
    public class Registeration
    {
        private Registeration()
        {

        }
        public Guid Id { get; private init; }
        public string Name { get; private init; }
        public PhoneNumber PhoneNumber { get; private init; }
        public Email Email { get; private init; }
        public Event Event { get; private init; }
        public static Registeration CreateRegisteration(Guid id, string name, PhoneNumber phoneNumber, Event @event, Email email)
        {
            var model = new Registeration()
            {
                Id = id,
                Name = name,
                PhoneNumber = phoneNumber,
                Event = @event,
                Email = email
            };

            return model;
        }

    }
}
