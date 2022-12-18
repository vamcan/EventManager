using EventManager.Core.Domain.Base.Exceptions;
using EventManager.Core.Domain.Entities.Event;
using EventManager.Core.Domain.Entities.User;
using EventManager.Core.Domain.ValueObjects;
using Xunit;

namespace EventManager.UnitTests.Domain
{
    public class EventTests
    {
        [Fact]
        public void CreateEvent_WithValidData_ReturnsEvent()
        {
            // Arrange
            var userName = "TestUserName";
            var user = User.CreateUser(Guid.NewGuid(), "test user", "TestUserName", userName,
                Email.CreateIfNotEmpty("test@gmail.com"));
            var id = Guid.NewGuid();
            var name = "Test Event";
            var description = "This is a test event";
            var location = "Test Location";
            var startTime = DateTime.Now;
            var endTime = startTime.AddHours(2);

            // Act
            var result = Event.CreateEvent(id, name, description, location, startTime, endTime, user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(description, result.Description);
            Assert.Equal(location, result.Location);
            Assert.Equal(startTime, result.StartTime);
            Assert.Equal(endTime, result.EndTime);
            Assert.Equal(user, result.User);
        }

        [Fact]
        public void CreateEvent_WithInvalidUser_ThrowsDomainStateException()
        {
            // Arrange

            User user = null;
            var id = Guid.NewGuid();
            var name = "Test Event";
            var description = "This is a test event";
            var location = "Test Location";
            var startTime = DateTime.Now;
            var endTime = startTime.AddHours(2);


            // Act and Assert
            var ex = Assert.Throws<DomainStateException>(() =>
                Event.CreateEvent(id, name, description, location, startTime, endTime, user));
            Assert.Equal("User does not exist", ex.Message);
        }

        [Fact]
        public void CreateEvent_WithEndTimeBeforeStartTime_ThrowsDomainStateException()
        {
            // Arrange
            var userName = "TestUserName";
            var user = User.CreateUser(Guid.NewGuid(), "test user", "TestUserName",
                userName, Email.CreateIfNotEmpty("test@gmail.com"));
            var id = Guid.NewGuid();
            var name = "Test Event";
            var description = "This is a test event";
            var location = "Test Location";
            var startTime = DateTime.Now;
            var endTime = startTime.AddHours(-2);


            // Act and Assert
            var ex = Assert.Throws<DomainStateException>(() =>
                Event.CreateEvent(id, name, description, location, startTime, endTime, user));
            Assert.Equal("The end time cannot be smaller than the start time", ex.Message);
        }

        [Fact]
        public void CreateEvent_WithEmptyId_ThrowsDomainStateException()
        {
            // Arrange
            var userName = "TestUserName";
            var user = User.CreateUser(Guid.NewGuid(), "test user", "TestUserName",
                userName, Email.CreateIfNotEmpty("test@gmail.com"));
            var id = Guid.Empty;
            var name = "Test Event";
            var description = "This is a test event";
            var location = "Test Location";
            var startTime = DateTime.Now;
            var endTime = startTime.AddHours(2);

            // Act and Assert
            var ex = Assert.Throws<DomainStateException>(() =>
                Event.CreateEvent(id, name, description, location, startTime, endTime, user));
            Assert.Equal("Event Id is not valid", ex.Message);

        }
    }
}
