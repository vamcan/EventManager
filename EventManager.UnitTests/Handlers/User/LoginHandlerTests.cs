using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManager.Core.Application.User.Login;
using EventManager.Core.Domain.Contracts.Repository;
using EventManager.Core.Domain.ValueObjects;
using Moq;
using Xunit;

namespace EventManager.UnitTests.Handlers.User
{
    public class LoginHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly LoginHandler _loginHandler;

        public LoginHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _loginHandler = new LoginHandler(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task Test_Handle_Success()
        {
            // Arrange
            var request = new LoginCommand
            {
                UserName = "johndoe",
                Password = "123456"
            };
            var user = Core.Domain.Entities.User.User.CreateUser(Guid.NewGuid(), "test User", "Test LastName"
                , request.UserName, Email.CreateIfNotEmpty("test@gmail.com"));
          user.SetPasswordHash(request.Password);
            _userRepositoryMock.Setup(x => x.FindByUserNameAsync(request.UserName, CancellationToken.None))
                .ReturnsAsync(user);

            // Act
            var result = await _loginHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(request.UserName, result.Result.UserName);
        }

        [Fact]
        public async Task Test_Handle_InvalidCredentials()
        {
            // Arrange
            var request = new LoginCommand
            {
                UserName = "johndoe",
                Password = "123456"
            };
            var user = Core.Domain.Entities.User.User.CreateUser(Guid.NewGuid(), "test User", "Test LastName"
                , request.UserName, Email.CreateIfNotEmpty("test@gmail.com"));
            _userRepositoryMock.Setup(x => x.FindByUserNameAsync(request.UserName, CancellationToken.None))
                .ReturnsAsync(user);

            // Act
            var result = await _loginHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid username or password.", result.ErrorMessage);
        }

        [Fact]
        public async Task Test_Handle_UserNotFound()
        {
            // Arrange
            var request = new LoginCommand
            {
                UserName = "johndoe",
                Password = "123456"
            };
            _userRepositoryMock.Setup(x => x.FindByUserNameAsync(request.UserName, CancellationToken.None))
                .ReturnsAsync((Core.Domain.Entities.User.User) null);

            // Act
            var result = await _loginHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid username or password.", result.ErrorMessage);
        }

    }
}


