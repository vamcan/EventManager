using EventManager.Core.Domain.Contracts.Repository;
using EventManager.Core.Domain.Entities.Event;
using EventManager.Infrastructure.Sql.Common;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Infrastructure.Sql.Repository
{
    internal class EventRepository : IEventRepository
    {
        private readonly EventDbContext _dbContext;

        public EventRepository(EventDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddEventAsync(Event @event, CancellationToken cancellationToken = default)
        {
            await _dbContext.Events.AddAsync(@event, cancellationToken);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

        public async Task<bool> UpdateEventAsync(Event @event, CancellationToken cancellationToken = default)
        {
            await _dbContext.Events.AddAsync(@event, cancellationToken);
            _dbContext.Entry(@event).State = EntityState.Modified;

            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

        public Task<List<Event>> GetAllEventsAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Events.OrderByDescending(c=>c.Created).ToListAsync(cancellationToken);
        }

        public Task<List<Registration>> GetEventRegistrationsAsync(Event @event, CancellationToken cancellationToken = default)
        {
            return _dbContext.Registrations.Where(c => c.Event.Id.Equals(@event.Id)).ToListAsync(cancellationToken);
        }


        public async Task<Event?> GetEventByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Events.FirstOrDefaultAsync(c => c.Id.Equals(id), cancellationToken);
        }
    }
}
