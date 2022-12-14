using EventManager.Core.Application.Auth;
using EventManager.Core.Application.Base.Common;
using EventManager.Core.Domain.Contracts.Repository;
using MediatR;

namespace EventManager.Core.Application.User.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, OperationResult<LoginResult>>
    {

        private readonly IUserRepository _userRepository;
        private readonly IJwtFactory _jwtFactory;


        public LoginHandler(IUserRepository userRepository, IJwtFactory jwtFactory)
        {
            _userRepository = userRepository;
            _jwtFactory = jwtFactory;
        }

        public async Task<OperationResult<LoginResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!string.IsNullOrEmpty(request.UserName) && !string.IsNullOrEmpty(request.Password))
                {
                    // confirm we have a user with the given name
                    var user = await _userRepository.FindByName(request.UserName, cancellationToken);
                    if (user != null)
                    {
                        // validate password
                        if (await _userRepository.CheckPassword(user, request.Password, cancellationToken))
                        {
                            // generate token
                            var token = await _jwtFactory.GenerateEncodedToken(user.Id, user.UserName);
                            var loginResult = new LoginResult() { Token = token };
                            return OperationResult<LoginResult>.SuccessResult(loginResult);
                        }
                    }
                }
                return OperationResult<LoginResult>.FailureResult("Invalid username or password."); ;
            }
            catch (Exception e)
            {
                return OperationResult<LoginResult>.FailureResult(e.Message);
            }
        }
    }
}

