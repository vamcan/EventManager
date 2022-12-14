using EventManager.Core.Application.User.Login;
using EventManager.Web.App.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace EventManager.Web.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController( IMediator mediator)
        {
         _mediator = mediator;
        }

        public IActionResult Index()
        {
           return View();
        }

        // POST: EventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return (RedirectToAction("Error"));
            }

            var result = await _mediator.Send(request, cancellationToken);
            if (result.IsSuccess)
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, result.Result.UserName),
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Event");
            }
            return (RedirectToAction("Error"));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
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