using EventManager.Core.Domain.Base.Exceptions;
using EventManager.Core.Domain.Entities.User;
using EventManager.Core.Domain.ValueObjects;
using Xunit;

namespace EventManager.UnitTests.Domain
{
    public class UserTests
    {
        [Fact]
        public void CreateUser_WithValidInput_CreatesUser()
        {
            // Arrange
            var id = Guid.NewGuid();
            var firstName = "reza";
            var lastName = "ghasemi";
            var userName = "testUser";
            var email = new Email("test@example.com");

            // Act
            var user = User.CreateUser(id, firstName, lastName, userName, email);

            // Assert
            Assert.Equal(id, user.Id);
            Assert.Equal(firstName, user.FirstName);
            Assert.Equal(lastName, user.LastName);
            Assert.Equal(userName, user.UserName);
            Assert.Equal(email, user.Email);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CreateUser_WithInvalidUserName_ThrowsDomainStateException(string userName)
        {
            // Arrange
            var id = Guid.NewGuid();
            var firstName = "reza";
            var lastName = "ghasemi";
            var email = new Email("test@example.com");

            // Act and Assert
            Assert.Throws<DomainStateException>(() => User.CreateUser(id, firstName, lastName, userName, email));
        }

        [Fact]
        public void CreateUser_WithInvalidId_ThrowsDomainStateException()
        {
            // Arrange
            var id = Guid.Empty;
            var firstName = "reza";
            var lastName = "ghasemi";
            var userName = "testUser";
            var email = new Email("test@example.com");

            // Act and Assert
            Assert.Throws<DomainStateException>(() => User.CreateUser(id, firstName, lastName, userName, email));
        }

        [Fact]
        public void SetPasswordHash_WithValidPassword_SetsHashedPassword()
        {
            // Arrange
            var user = User.CreateUser(Guid.NewGuid(), "reza", "ghasemi", "testUsername", new Email("test@example.com"));
            var password = "testP@ssw0rd";

            // Act
            user.SetPasswordHash(password);

            // Assert
            Assert.NotNull(user.HashedPassword);
            Assert.NotEqual(password, user.HashedPassword);
        }
    }
}
