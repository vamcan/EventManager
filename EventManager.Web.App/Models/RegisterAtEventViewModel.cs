using EventManager.Core.Application.Event.GetEvent;
using EventManager.Core.Application.Event.RegisterInEvent;

namespace EventManager.Web.App.Models
{
    public class RegisterInEventViewModel
    {
        public RegisterInEventCommand RegisterInEventCommand { get; set; }
        public GetEventResult GetEventResult { get; set; }
    }
}
