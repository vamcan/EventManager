using EventManager.Core.Domain.Contracts.Repository;
using EventManager.Core.Domain.Entities.Event;
using EventManager.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Infrastructure.Repository
{
    internal class EventRepository : IEventRepository
    {
        private readonly EventDbContext _dbContext;

        public EventRepository(EventDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Event> AddEventAsync(Event @event, CancellationToken cancellationToken = default)
        {
            await _dbContext.Events.AddAsync(@event, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return @event;
        }

        public async Task<Event> UpdateEventAsync(Event @event, CancellationToken cancellationToken = default)
        {
            await _dbContext.Events.AddAsync(@event, cancellationToken);
            _dbContext.Entry(@event).State = EntityState.Modified;
            
            await _dbContext.SaveChangesAsync(cancellationToken);
            return @event;
        }

        public Task<List<Event>> GetAllEventsAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Events.ToListAsync(cancellationToken);
        }

        public async Task<Event> RegisterAtEventAsync(Event @event, CancellationToken cancellationToken = default)
        {
            await _dbContext.Events.AddAsync(@event, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return @event;
        }

        public async Task<Event?> GetEventByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Events.FirstOrDefaultAsync(c => c.Id.Equals(id), cancellationToken);
        }
    }
}
