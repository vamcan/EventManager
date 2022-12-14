using EventManager.Core.Application.Base.Common;
using MediatR;

namespace EventManager.Core.Application.User.Login
{
    public class LoginCommand : IRequest<OperationResult<LoginResult>>
    {
        public string UserName { get; }
        public string Password { get; }
    }
}
