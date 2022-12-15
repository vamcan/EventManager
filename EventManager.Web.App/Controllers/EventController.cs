using EventManager.Core.Application.Event.GetAllEvents;
using EventManager.Web.App.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        // GET: EventController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EventController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
