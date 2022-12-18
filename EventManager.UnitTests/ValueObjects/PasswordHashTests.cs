using EventManager.Core.Domain.Base.Exceptions;
using EventManager.Core.Domain.ValueObjects;
using Xunit;

namespace EventManager.UnitTests.ValueObjects
{
    public class PasswordHashTests
    {
        [Fact]
        public void PasswordHash_ThrowsException_WhenGivenEmptyPassword()
        {
            // Arrange
            string emptyPassword = "";
            string whitespacePassword = " ";

            // Act & Assert
            Assert.Throws<InvalidValueObjectStateException>(() => new PasswordHash(emptyPassword));
            Assert.Throws<InvalidValueObjectStateException>(() => new PasswordHash(whitespacePassword));
        }
        [Fact]
        public void HashPassword_ReturnsExpectedHash_ForGivenPassword()
        {
            // Arrange
            string password = "password123";
            string expectedHash = "FE1F47D38EB7B2FCD97E447D88E0BA2CF8ABCD1683733223FCA9A0366AAF7261";

            // Act
            string actualHash = PasswordHash.CreateIfNotEmpty(password).Value;

            // Assert
            Assert.Equal(expectedHash, actualHash);
        }
        [Fact]
        public void ObjectIsEqual_ReturnsTrue_WhenGivenEqualPasswordHashes()
        {
            // Arrange
            string password = "password123";
            PasswordHash passwordHash1 = new PasswordHash(password);
            PasswordHash passwordHash2 = new PasswordHash(password);

            // Act
            bool areEqual = passwordHash1.ObjectIsEqual(passwordHash2);

            // Assert
            Assert.True(areEqual);
        }

        [Fact]
        public void ObjectIsEqual_ReturnsFalse_WhenGivenDifferentPasswordHashes()
        {
            // Arrange
            PasswordHash passwordHash1 = new PasswordHash("password123");
            PasswordHash passwordHash2 = new PasswordHash("password456");

            // Act
            bool areEqual = passwordHash1.ObjectIsEqual(passwordHash2);

            // Assert
            Assert.False(areEqual);
        }
    }
}
