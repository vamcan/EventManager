using EventManager.Core.Application.Base.Common;
using MediatR;

namespace EventManager.Core.Application.User.Login
{
    public  class LoginCommand : IRequest<OperationResult<LoginResult>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
