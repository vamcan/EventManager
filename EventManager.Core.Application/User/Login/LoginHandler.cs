using EventManager.Core.Application.Auth;
using EventManager.Core.Application.Base.Common;
using EventManager.Core.Domain.Contracts.Repository;
using EventManager.Core.Domain.ValueObjects;
using MediatR;

namespace EventManager.Core.Application.User.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, OperationResult<LoginResult>>
    {

        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;


        public LoginHandler(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<OperationResult<LoginResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!string.IsNullOrEmpty(request.UserName) && !string.IsNullOrEmpty(request.Password))
                {
                    // confirm we have a user with the given name
                    var user = await _userRepository.FindByUserNameAsync(request.UserName, cancellationToken);
                    if (user != null)
                    {
                        // validate password
                        if (PasswordHash.CreateIfNotEmpty(request.Password).Value.Equals(user.HashedPassword))
                        {
                            // generate token
                            var token = await _tokenService.BuildTokenAsync(request);
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

