using EventManager.Core.Domain.Base.Exceptions;
using EventManager.Core.Domain.Entities.Event;
using EventManager.Core.Domain.ValueObjects;
using Xunit;

namespace EventManager.UnitTests.Domain
{
    public class RegistrationTests
    {
        [Fact]
        public void CreateRegistration_WithValidData_ReturnsRegistration()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Test User";
            var phoneNumber = new PhoneNumber("123-456-7890");
            var email = new Email("test@example.com");

            var user = Core.Domain.Entities.User.User.CreateUser(Guid.NewGuid(), "test user", "Test Family", "TestUsername", Email.CreateIfNotEmpty("test@gmail.com"));
            var @event = Event.CreateEvent(Guid.NewGuid(), "Test Event", "This is a test event", "Test location",
                DateTime.Now, DateTime.Now.AddHours(1), user);
         

            // Act
            var result = Registration.CreateRegistration(id, name, phoneNumber, @event, email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(phoneNumber, result.PhoneNumber);
            Assert.Equal(email, result.Email);
            Assert.Equal(@event, result.Event);
        }

        [Fact]
        public void CreateRegistration_WithInvalidEvent_ThrowsDomainStateException()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Test User";
            var phoneNumber = new PhoneNumber("123-456-7890");
            var email = new Email("test@example.com");
            Event @event = null;

            // Act and Assert
            var ex = Assert.Throws<DomainStateException>(() => Registration.CreateRegistration(id, name, phoneNumber, @event, email));
            Assert.Equal("Event does not exist", ex.Message);
        }

        [Fact]
        public void CreateRegistration_WithEmptyId_ThrowsDomainStateException()
        {
            // Arrange
            var id = Guid.Empty;
            var name = "Test User";
            var phoneNumber = new PhoneNumber("123-456-7890");
            var email = new Email("test@example.com");
            var user = Core.Domain.Entities.User.User.CreateUser(Guid.NewGuid(), "test user", "Test Family", "TestUsername", Email.CreateIfNotEmpty("test@gmail.com"));
            var @event = Event.CreateEvent(Guid.NewGuid(), "Test Event", "This is a test event", "Test location",
                DateTime.Now, DateTime.Now.AddHours(1), user);

            // Act and Assert
            var ex = Assert.Throws<DomainStateException>(() => Registration.CreateRegistration(id, name, phoneNumber, @event, email));
            Assert.Equal("Registration Id is not valid", ex.Message);
        }
    }
}
