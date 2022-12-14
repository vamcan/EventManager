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
                var user = Domain.Entities.User.User.CreateUser(Guid.NewGuid(), request.FirstName, request.LastName,
                    request.UserName, request.Email);
                var result = await _userRepository.AddUserAsync(user, request.Password);
                if (result == null)
                {
                    return OperationResult<AddUserResult>.FailureResult("user not registered."); ;
                }

                return OperationResult<AddUserResult>.SuccessResult(new AddUserResult() { User = user });
            }
            catch (Exception e)
            {
                return OperationResult<AddUserResult>.FailureResult(e.Message);
            }
        }
    }
}

