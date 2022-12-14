using EventManager.Core.Domain.Contracts.Repository;
using EventManager.Core.Domain.Entities.User;
using EventManager.Infrastructure.Identity.Model;
using Microsoft.AspNetCore.Identity;

namespace EventManager.Infrastructure.Identity.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> AddUserAsync(User user, string password)
        {
            var applicationUser = new ApplicationUser()
            {
                Id = user.Id.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName
            };
            var identityResult = await _userManager.CreateAsync(applicationUser, password);
            if (identityResult.Succeeded)
            {
                return user;
            }

            return null!;
        }


        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            var identityUser = await _userManager.FindByIdAsync(userId.ToString());
            if (identityUser == null)
            {
                return null!;
            }

            var user = User.CreateUser(new Guid(identityUser.Id), identityUser.FirstName, identityUser.LastName,
                identityUser.UserName, identityUser.Email);
            return user;
        }

        public async Task<User> FindByName(string userName)
        {
            var identityUser = await _userManager.FindByNameAsync(userName);
            if (identityUser == null)
            {
                return null!;
            }

            var user = User.CreateUser(new Guid(identityUser.Id), identityUser.FirstName, identityUser.LastName,
                identityUser.UserName, identityUser.Email);
            return user;
        }

        public async Task<bool> CheckPassword(User user, string password)
        {
            var applicationUser = new ApplicationUser()
            {
                Id = user.Id.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };
            return await _userManager.CheckPasswordAsync(applicationUser, password);
        }
    }
}
