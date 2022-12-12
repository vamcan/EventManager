using EventManager.Core.Domain.Entities.Event;

namespace EventManager.Core.Domain.Contracts.Repository
{
    public interface IEventRepository
    {
        Task<Event> AddEventAsync(Event @event, CancellationToken cancellationToken = default);
        Task<Event> UpdateEventAsync(Event @event, CancellationToken cancellationToken = default);
        Task<List<Event>> GetAllEventsAsync(CancellationToken cancellationToken = default);
        Task<Event> RegisterAtEventAsync(Event @event, CancellationToken cancellationToken = default);
        Task<Event> GetEventByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
