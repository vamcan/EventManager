using System.Globalization;
using EventManager.Core.Application.Event.GetEventRegistrations;
using EventManager.Core.Domain.Entities.Event;

namespace EventManager.Core.Application.Mapper
{
    public class RegistrationMapper
    {
        public static EventRegistrationDto RegistrationToEventRegistrationDtoMapper(Registration item)
        {
            return new EventRegistrationDto()
            {
                Name = item.Name,
                Email = item.Email.Value,
                PhoneNumber = item.PhoneNumber.Value,
                RegistrationDate = item.Created.ToString(CultureInfo.InvariantCulture)
            };
        }
    }
}
