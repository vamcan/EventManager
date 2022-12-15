using EventManager.Core.Domain.Entities.Event;

namespace EventManager.Core.Domain.Contracts.Repository
{
    public interface IEventRepository
    {
        Task<bool> AddEventAsync(Event @event, CancellationToken cancellationToken = default);
        Task<bool> UpdateEventAsync(Event @event, CancellationToken cancellationToken = default);
        Task<List<Event>> GetAllEventsAsync(CancellationToken cancellationToken = default);
        Task<bool> RegisterAtEventAsync(Event @event, CancellationToken cancellationToken = default);
        Task<Event?> GetEventByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
