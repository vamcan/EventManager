using EventManager.Core.Domain.Contracts.Repository;
using EventManager.Core.Domain.Entities.User;
using EventManager.Infrastructure.Auth;
using EventManager.Infrastructure.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly EventDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(EventDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<User> AddUserAsync(User user, string password, CancellationToken cancellationToken = default)
        {
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return user;
        }

        public Task<User?> GetUserByIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return _dbContext.Users.FirstOrDefaultAsync(c => c.Id.Equals(userId), cancellationToken);
        }

        public Task<User> FindByName(string userName, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckPassword(User user, string password, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
