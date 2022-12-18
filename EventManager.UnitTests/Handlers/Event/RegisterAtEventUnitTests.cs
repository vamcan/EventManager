using EventManager.Core.Application.Event.RegisterAtEvent;
using EventManager.Core.Domain.Contracts.Repository;
using Moq;
using EventManager.Core.Domain.ValueObjects;
using Xunit;
using EventManager.Core.Domain.Entities.User;

namespace EventManager.UnitTests.Handlers.Event
{
    public class RegisterAtEventHandlerTests
    {
        private readonly Mock<IEventRepository> _eventRepositoryMock;
        private readonly RegisterAtEventHandler _handler;

        public RegisterAtEventHandlerTests()
        {
            _eventRepositoryMock = new Mock<IEventRepository>();
            _handler = new RegisterAtEventHandler(_eventRepositoryMock.Object);
        }

        [Fact]
        public async Task TestRegisterAtEventHandler()
        {
            // Set up test data
            var request = new RegisterAtEventCommand
            {
                EventId = Guid.NewGuid(),
                Name = "Reza Ghasemi",
                PhoneNumber = "123-456-7890",
                Email = "reza.ghasemi@example.com"
            };
            var user = Core.Domain.Entities.User.User.CreateUser(Guid.NewGuid(), "test user", "Test Family", "TestUsername", Email.CreateIfNotEmpty("test@gmail.com"));
            var currentEvent = Core.Domain.Entities.Event.Event.CreateEvent(request.EventId, "Test Event", "This is a test event", "Test location",
                DateTime.Now, DateTime.Now.AddHours(1), user);

            // Set up mock behavior
            _eventRepositoryMock
                .Setup(repo => repo.GetEventByIdAsync(request.EventId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(currentEvent);

            // Execute the method being tested
            var result = await _handler.Handle(request, CancellationToken.None);

            // Verify the result
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
            Assert.Equal(request.Name, result.Result.Name);
            Assert.Equal(request.PhoneNumber, result.Result.PhoneNumber);
            Assert.Equal(request.Email, result.Result.Email);
            Assert.Equal(currentEvent.Name, result.Result.EventName);
            Assert.Equal(currentEvent.StartTime, result.Result.EventStartTime);
            Assert.Equal(currentEvent.EndTime, result.Result.EventEndTime);
        }

        [Fact]
        public async Task RegisterAtEventHandler_Should_Not_Add_Event_If_IEventRepository_Throws_An_Exception()
        {
            // Arrange
            var request = new RegisterAtEventCommand()
            {
                EventId = Guid.NewGuid(),
                Name = "Reza Ghasemi",
                PhoneNumber = "123-456-7890",
                Email = "reza.ghasemi@example.com"
            };
            _eventRepositoryMock.Setup(r => r.GetEventByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception("Test exception"));

            // Act
            var result = _handler.Handle(request, CancellationToken.None).Result;

            // Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task RegisterAtEventHandler_Should_Not_Register_Event_If_Event_Does_Not_Exist()
        {
            var eventId = Guid.NewGuid();
            _eventRepositoryMock.Setup(r => r.GetEventByIdAsync(eventId, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Core.Domain.Entities.Event.Event>(null));

            var request = new RegisterAtEventCommand()
            {
                EventId = eventId,
                Name = "Reza Ghasemi",
                PhoneNumber = "123-456-7890",
                Email = "reza.ghasemi@example.com"
            };

            // Act
            var result = _handler.Handle(request, CancellationToken.None).Result;

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Event does not exist", result.ErrorMessage);
        }
    }
}