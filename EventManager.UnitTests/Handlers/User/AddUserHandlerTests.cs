using EventManager.Core.Application.User.AddUser;
using EventManager.Core.Domain.Contracts.Repository;
using Moq;
using Xunit;

namespace EventManager.UnitTests.Handlers.User
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventManager.Core.Domain.ValueObjects;
    using Moq;
    using Xunit;

    namespace MyProject.Tests
    {
        public class AddUserHandlerTests
        {
            private readonly Mock<IUserRepository> _userRepositoryMock;
            private readonly AddUserHandler _addUserHandler;

            public AddUserHandlerTests()
            {
                _userRepositoryMock = new Mock<IUserRepository>();
                _addUserHandler = new AddUserHandler(_userRepositoryMock.Object);
            }

            [Fact]
            public async Task Handle_ShouldReturnSuccessResult_WhenUserIsAddedSuccessfully()
            {
                // Arrange
                var request = new AddUserCommand
                {
                    FirstName = "test FirstName",
                    LastName = "test LastName",
                    UserName = "TestUser",
                    Email = "test@gmail.com",
                    Password = "password123"
                };

                var expectedUser = EventManager.Core.Domain.Entities.User.User.CreateUser(Guid.NewGuid(),
                    request.FirstName, request.LastName, request.UserName, Email.CreateIfNotEmpty(request.Email));
                expectedUser.SetPasswordHash(request.Password);

                _userRepositoryMock.Setup(r => r.AddUserAsync(It.IsAny<Core.Domain.Entities.User.User>(), 
                    It.IsAny<CancellationToken>())).ReturnsAsync(true);

                // Act
                var result = await _addUserHandler.Handle(request, CancellationToken.None);

                // Assert
                Assert.True(result.IsSuccess);
                Assert.IsType<AddUserResult>(result.Result);
                Assert.Equal(expectedUser.Email, result.Result.User.Email);
                Assert.Equal(expectedUser.HashedPassword, result.Result.User.HashedPassword);
                Assert.Equal(expectedUser.UserName, result.Result.User.UserName);
                Assert.Equal(expectedUser.FirstName, result.Result.User.FirstName);
                Assert.Equal(expectedUser.LastName, result.Result.User.LastName);
            }

            [Fact]
            public async Task Handle_ShouldReturnFailureResult_WhenUserFailsToRegister()
            {
                // Arrange
                var request = new AddUserCommand
                {
                    FirstName = "test FirstName",
                    LastName = "test LastName",
                    UserName = "TestUser",
                    Email = "test@gmail.com",
                    Password = "password123"
                };

                var expectedUser = EventManager.Core.Domain.Entities.User.User.CreateUser(Guid.NewGuid(),
                    request.FirstName, request.LastName, request.UserName, Email.CreateIfNotEmpty(request.Email));
                expectedUser.SetPasswordHash(request.Password);

                _userRepositoryMock.Setup(r => r.AddUserAsync(It.IsAny<Core.Domain.Entities.User.User>(), CancellationToken.None))
                    .ReturnsAsync(false);

                // Act
                var result = await _addUserHandler.Handle(request, CancellationToken.None);

                // Assert
                Assert.False(result.IsSuccess);
                Assert.Equal("User failed to register.", result.ErrorMessage);
            }
        }
    }
}