using EventManager.Core.Application.Auth;
using EventManager.Core.Application.Base.Common;
using EventManager.Core.Application.User.Login;
using EventManager.Core.Domain.Contracts.Repository;
using MediatR;

namespace EventManager.Core.Application.User.AddUser
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, OperationResult<AddUserResult>>
    {

        private readonly IUserRepository _userRepository;
        private readonly IJwtFactory _jwtFactory;


        public AddUserHandler(IUserRepository userRepository, IJwtFactory jwtFactory)
        {
            _userRepository = userRepository;
            _jwtFactory = jwtFactory;
        }

        public async Task<OperationResult<AddUserResult>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!string.IsNullOrEmpty(request.UserName) && !string.IsNullOrEmpty(request.Password))
                {
                    // confirm we have a user with the given name
                    var user = await _userRepository.FindByName(request.UserName);
                    if (user != null)
                    {
                        // validate password
                        if (await _userRepository.CheckPassword(user, request.Password))
                        {
                            // generate token
                            var token = await _jwtFactory.GenerateEncodedToken(user.Id, user.UserName);
                            var loginResult = new AddUserResult() { User = user};
                            return OperationResult<AddUserResult>.SuccessResult(loginResult);
                        }
                    }
                }
                return OperationResult<AddUserResult>.FailureResult("Invalid username or password."); ;
            }
            catch (Exception e)
            {
                return OperationResult<AddUserResult>.FailureResult(e.Message);
            }
        }
    }
}

