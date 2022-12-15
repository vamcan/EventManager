using EventManager.Core.Domain.Base;
using EventManager.Core.Domain.ValueObjects;

namespace EventManager.Core.Domain.Entities.User
{
    public class User : IBaseEntity, IAggregateRoot
    {
        private User()
        {
        }
        public Guid Id { get; private init; }
        public string FirstName { get; private init; }
        public string LastName { get; private init; }
        public string UserName { get; private init; }
        public Email Email { get; private init; }
        public string HashedPassword { get; private set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public static User CreateUser(Guid id, string firstName, string lastName, string userName, Email email)
        {
            var model = new User()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Email = email
            };
            return model;
        }

        public void SetPasswordHash(string password)
        {
            HashedPassword = PasswordHash.CreateIfNotEmpty(password).Value;
        }
    }
}
