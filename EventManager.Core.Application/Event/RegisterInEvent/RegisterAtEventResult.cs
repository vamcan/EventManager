using EventManager.Core.Domain.ValueObjects;

namespace EventManager.Core.Application.Event.RegisterInEvent
{
    public class RegisterInEventResult
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string EventName { get; set;}
        public DateTime EventStartTime { get; set;}
        public DateTime EventEndTime { get; set;}

    }
}
