using EventManager.Core.Application.Event.AddEvent;
using EventManager.Core.Application.User.AddUser;
using EventManager.Core.Application.User.Login;
using EventManager.Core.Domain.Entities.User;
using EventManager.Core.Domain.ValueObjects;
using EventManager.IntegrationTests.Base;

namespace EventManager.IntegrationTests
{
    public class UserIntegrationTests : IClassFixture<BaseRepositoryFixture>
    {

        private readonly BaseRepositoryFixture _factory;
        public UserIntegrationTests(BaseRepositoryFixture factory)
        {
            _factory = factory;

        }
        [Fact]
        public async Task AddUser_WithValidInput_ReturnsSuccessResult()
        {
            // Arrange
            _factory.Build();
            var handler = new AddUserHandler(_factory.UserRepository);
            // Arrange
            var request = new AddUserCommand
            {
                FirstName = "test FirstName",
                LastName = "test LastName",
                UserName = "TestUser",
                Email = "test@gmail.com",
                Password = "password123"
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.IsType<AddUserResult>(result.Result);
            Assert.Equal(request.Email, result.Result.User.Email.Value);
            Assert.Equal(PasswordHash.CreateIfNotEmpty(request.Password).Value, result.Result.User.HashedPassword);
            Assert.Equal(request.UserName, result.Result.User.UserName);
            Assert.Equal(request.FirstName, result.Result.User.FirstName);
            Assert.Equal(request.LastName, result.Result.User.LastName);
        }


        [Fact]
        public async Task Login_Handle_Success()
        {
            // Arrange
            _factory.Build();
            var handler = new LoginHandler(_factory.UserRepository);
            var request = new LoginCommand
            {
                UserName = "test",
                Password = "123456"
            };
            var user = User.CreateUser(Guid.NewGuid(), "test User", "Test LastName"
                , request.UserName, Email.CreateIfNotEmpty("test@gmail.com"));
            user.SetPasswordHash(request.Password);

            await _factory.UserRepository.AddUserAsync(user, CancellationToken.None);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(request.UserName, result.Result.UserName);
        }

    }
}
