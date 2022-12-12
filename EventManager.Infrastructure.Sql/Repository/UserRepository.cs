using EventManager.Core.Domain.Contracts.Repository;
using EventManager.Core.Domain.Entities.User;
using EventManager.Infrastructure.Sql.Common;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Infrastructure.Sql.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly EventDbContext _dbContext;

        public UserRepository(EventDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AddUserAsync(User user, CancellationToken cancellationToken = default)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return user;
        }

        public Task<User?> GetUserByIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return _dbContext.Users.FirstOrDefaultAsync(c => c.Id.Equals(userId), cancellationToken);
        }
    }
}
