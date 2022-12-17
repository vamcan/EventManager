using EventManager.Core.Application.Event.GetAllEvents;
using EventManager.Core.Application.Event.GetEvent;
using EventManager.Web.App.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EventManager.Web.App.Controllers
{
    public class EventRegistration : Controller
    {
        private readonly IMediator _mediator;

        public EventRegistration(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var query = new GetAllEventsQuery();
            var events = await _mediator.Send(query);
            if (events.IsSuccess)
            {
                return View(events.Result);
            }
            return (RedirectToAction("Error"));
        }

        public async Task<IActionResult> Register(Guid id)
        {
            var query = new GetEventQuery() { EventId = id };
            var @event = await _mediator.Send(query);
            if (@event.IsSuccess)
            {
                return View(new RegisterAtEventViewModel() { GetEventResult = @event.Result });
            }
            return (RedirectToAction("Error"));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterAtEventViewModel request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return (RedirectToAction("Error"));
            }

            var result = await _mediator.Send(request.RegisterAtEventCommand, cancellationToken);
            if (result.IsSuccess)
            {
                return RedirectToAction("RegisterSucceed");
            }
            return (RedirectToAction("Error"));
        }

        public IActionResult RegisterSucceed()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
    