using EventManager.Core.Domain.Base;

namespace EventManager.Core.Domain.Entities.User
{
    public class User : IAggregateRoot
    {
        private User()
        {
        }
        public int Id { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string UserName { get; }
        public string PasswordHash { get; }
    }
}
