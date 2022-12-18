using EventManager.Core.Domain.Base;
using EventManager.Core.Domain.Base.Exceptions;
using EventManager.Core.Domain.ValueObjects;

namespace EventManager.Core.Domain.Entities.Event
{
    public class Registration : IBaseEntity
    {
        private Registration()
        {

        }
        public Guid Id { get; private init; }
        public string Name { get; private init; }
        public PhoneNumber PhoneNumber { get; private init; }
        public Email Email { get; private init; }
        public Event Event { get; private init; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public static Registration CreateRegistration(Guid id, string name, PhoneNumber phoneNumber, Event @event, Email email)
        {
            if (@event == null)
            {
                throw new DomainStateException("Event does not exist");
            }

            if (id==Guid.Empty)
            {
                throw new DomainStateException("Registration Id is not valid");
            }
            var model = new Registration()
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
