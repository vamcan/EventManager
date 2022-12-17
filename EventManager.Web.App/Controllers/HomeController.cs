using EventManager.Core.Application.User.Login;
using EventManager.Web.App.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace EventManager.Web.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public HomeController(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
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
                var jwtsecuritytoken = new JwtSecurityTokenHandler().ReadToken(result.Result.Token) as JwtSecurityToken;
                var username = jwtsecuritytoken.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Name).Value;

                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),

                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Event");
            }
            return (RedirectToAction("Error"));
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