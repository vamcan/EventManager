namespace EventManager.Core.Application.Event.GetEventRegistrations
{
    public class GetEventRegistrationsResult
    {
        public string EventName { get; set; }
        public List<EventRegistrationDto> EventRegistrations { get; set; }
    }

    public class EventRegistrationDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string RegistrationDate { get; set; }
    }
}
