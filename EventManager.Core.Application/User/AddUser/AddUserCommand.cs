using EventManager.Core.Application.Base.Common;
using MediatR;

namespace EventManager.Core.Application.User.AddUser
{
    public class AddUserCommand : IRequest<OperationResult<AddUserResult>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
