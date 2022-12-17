using EventManager.Core.Application.Event.GetEvent;
using EventManager.Core.Application.Event.RegisterAtEvent;

namespace EventManager.Web.App.Models
{
    public class RegisterAtEventViewModel
    {
        public RegisterAtEventCommand RegisterAtEventCommand { get; set; }
        public GetEventResult GetEventResult { get; set; }
    }
}
