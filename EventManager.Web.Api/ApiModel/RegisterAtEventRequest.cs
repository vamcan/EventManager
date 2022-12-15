using EventManager.Core.Domain.ValueObjects;

namespace EventManager.Web.Api.ApiModel
{
    public class RegisterAtEventRequest
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
