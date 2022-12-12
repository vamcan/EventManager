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
        private readonly IMediator _mediatR;

        public EventController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventRequest request, CancellationToken cancellationToken)
        {
            var addEventCommand = new AddEventCommand()
            {
               Name = request.Name,
               Description = request.Description,
               EndTime = request.EndTime,
               StartTime = request.StartTime,
               Location = request.Location,
               UserId = 1
            };
            var addEvent = await _mediatR.Send(addEventCommand, cancellationToken);
            if (addEvent.IsSuccess)
            {
                return Ok(addEvent.Result);
            }

            return BadRequest(addEvent.ErrorMessage);
        }
        [HttpPost("RegisterAtEvent")]
        public async Task<IActionResult> RegisterAtEvent([FromBody] RegisterAtEventRequest request, CancellationToken cancellationToken)
        {
            var registerAtEventCommand = new RegisterAtEventCommand()
            {
               Name = request.Name,
               Email = Email.CreateIfNotEmpty(request.Email),
               PhoneNumber = PhoneNumber.CreateIfNotEmpty(request.PhoneNumber),
               EventId = new Guid("C926BF00-14A7-4CA8-97E3-143B117BD3D9")

            };
            var addEvent = await _mediatR.Send(registerAtEventCommand, cancellationToken);
            if (addEvent.IsSuccess)
            {
                return Ok(addEvent.Result);
            }

            return BadRequest(addEvent.ErrorMessage);
        }
    }
}
