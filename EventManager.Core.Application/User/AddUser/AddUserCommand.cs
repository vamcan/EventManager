using EventManager.Core.Application.Base.Common;
using EventManager.Core.Application.User.AddUser;
using MediatR;

namespace EventManager.Core.Application.User.Login
{
    public class AddUserCommand : IRequest<OperationResult<AddUserResult>>
    {
        public string UserName { get; }
        public string Password { get; }
    }
}
