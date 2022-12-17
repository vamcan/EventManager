namespace EventManager.Core.Application.Event.GetEvent
{
    public class GetEventResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
