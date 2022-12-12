using EventManager.Core.Domain.Base;

namespace EventManager.Core.Domain.Entities.User
{
    public class User : IAggregateRoot
    {
        private User()
        {
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public static User CreateUser(int id, string name, string userName, string password)
        {
            var model = new User()
            {
                Id = id,
                Name = name,
                UserName = userName,
                Password = password
            };
            return model;
        }


    }
}
