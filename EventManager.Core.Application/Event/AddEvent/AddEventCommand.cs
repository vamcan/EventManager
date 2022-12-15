using EventManager.Core.Application.Base.Common;
using MediatR;

namespace EventManager.Core.Application.Event.AddEvent
{
    public class AddEventCommand: IRequest<OperationResult<AddEventResult>>
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
