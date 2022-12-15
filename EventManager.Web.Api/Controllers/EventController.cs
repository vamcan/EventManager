using EventManager.Core.Application.Event.AddEvent;
using EventManager.Core.Application.Event.RegisterAtEvent;
using EventManager.Core.Domain.ValueObjects;
using EventManager.Web.Api.ApiModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        private readonly IMediator _mediator;

        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventRequest request, CancellationToken cancellationToken)
        {
            //TODO Get User From Token
            var addEventCommand = new AddEventCommand()
            {
               Name = request.Name,
               Description = request.Description,
               EndTime = request.EndTime,
               StartTime = request.StartTime,
               Location = request.Location,
               UserName = "reza"
            };
            var result = await _mediator.Send(addEventCommand, cancellationToken);
            return Ok(result);
        }
        [HttpPost("RegisterAtEvent")]
        public async Task<IActionResult> RegisterAtEvent([FromBody] RegisterAtEventRequest request, CancellationToken cancellationToken)
        {
            var registerAtEventCommand = new RegisterAtEventCommand()
            {
               Name = request.Name,
               Email = request.Email,
               PhoneNumber = request.PhoneNumber,
               EventId = request.EventId,

            };
            var result = await _mediator.Send(registerAtEventCommand, cancellationToken);
            return Ok(result);
        }
    }
}
