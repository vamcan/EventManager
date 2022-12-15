using EventManager.Core.Application.Event.GetEvent;

namespace EventManager.Core.Application.Event.Mapper
{
    public class EventMapper
    {
        public static GetEventResult EventToGetEventResultMapper(Domain.Entities.Event.Event item)
        {
            return new GetEventResult()
            {
                Description = item.Description,
                EndTime = item.EndTime,
                Id = item.Id,
                Location = item.Location,
                Name = item.Name,
                StartTime = item.StartTime
            };
        }
    }
}
