using EventManager.Core.Application.Base.Common;
using EventManager.Core.Application.User.Login;
using EventManager.Core.Domain.Contracts.Repository;
using EventManager.Core.Domain.ValueObjects;
using MediatR;

namespace EventManager.Core.Application.User.AddUser
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, OperationResult<AddUserResult>>
    {

        private readonly IUserRepository _userRepository;


        public AddUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult<AddUserResult>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = Domain.Entities.User.User.CreateUser(Guid.NewGuid(), request.FirstName, request.LastName,
                    request.UserName, Email.CreateIfNotEmpty(request.Email));
                user.SetPasswordHash(request.Password);
                var result = await _userRepository.AddUserAsync(user);
                if (result == null)
                {
                    return OperationResult<AddUserResult>.FailureResult("User failed to register."); ;
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

