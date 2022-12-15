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

        public async Task<bool> AddUserAsync(User user, CancellationToken cancellationToken = default)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

        public async Task<User?> FindByIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(c => c.Id.Equals(userId), cancellationToken);
            return user;
        }

        public async Task<User?> FindByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(c => c.UserName.Equals(userName), cancellationToken);
            return user;
        }
    }
}
