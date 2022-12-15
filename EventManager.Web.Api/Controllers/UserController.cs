using EventManager.Core.Application.User.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginCommand request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }

            var result =await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] AddUserCommand request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }

            var result =await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }
    }
}
