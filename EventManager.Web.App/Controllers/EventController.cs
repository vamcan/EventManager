using EventManager.Core.Application.Event.GetAllEvents;
using EventManager.Web.App.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EventManager.Core.Application.Event.GetEvent;

namespace EventManager.Web.App.Controllers
{
    //[Authorize]
    public class EventController : Controller
    {
        private readonly IMediator _mediator;
        // GET: EventController
        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var query = new GetAllEventsQuery();
            var events =await _mediator.Send(query);
            if (events.IsSuccess)
            {
                return View(events.Result);
            }
            return (RedirectToAction("Error"));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var query = new GetEventQuery(){EventId = id};
            var @event = await _mediator.Send(query);
            if (@event.IsSuccess)
            {
                return View(@event.Result);
            }
            return (RedirectToAction("Error"));
        }

        // GET: EventController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateEventViewModel eventViewModel,CancellationToken cancellationToken)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
