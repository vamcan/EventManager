using Microsoft.AspNetCore.Mvc;

namespace EventManager.Web.App.Controllers
{
    public class EventController : Controller
    {
        // GET: EventController
        public ActionResult Index()
        {
            return View();
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

      

      

    }
}
