using EventManager.Core.Application.Event.GetEventRegistrations;
using EventManager.Core.Domain.Contracts.Repository;
using EventManager.Core.Domain.Entities.Event;
using EventManager.Core.Domain.Entities.User;
using EventManager.Core.Domain.ValueObjects;
using Moq;
using Xunit;

namespace EventManager.UnitTests.Handlers.Event
{
    public class GetEventRegistrationsHandlerTests
    {
        private readonly Mock<IEventRepository> _eventRepositoryMock;
        private readonly GetEventRegistrationsHandler _handler;
        private readonly EventRegistrationsQuery _request;

        public GetEventRegistrationsHandlerTests()
        {
            _eventRepositoryMock = new Mock<IEventRepository>();
            _handler = new GetEventRegistrationsHandler(_eventRepositoryMock.Object);
            _request = new EventRegistrationsQuery { EventId = Guid.NewGuid() };
        }

        [Fact]
        public async Task Handle_ReturnsSuccessResult_WhenRepositoryReturnsEventAndRegistrations()
        {
            // Arrange
            var userName = "TestUserName";
            var user = Core.Domain.Entities.User.User.CreateUser(Guid.NewGuid(), "test user", "TestUserName", userName, Email.CreateIfNotEmpty("test@gmail.com"));
           
            var eventId = Guid.NewGuid();
            var @event = Core.Domain.Entities.Event.Event.CreateEvent(eventId, "Test Event", "This is a test event", "Test location",
                DateTime.Now, DateTime.Now.AddHours(1), user);
            _eventRepositoryMock.Setup(repo => repo.GetEventByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(@event);
            _eventRepositoryMock.Setup(repo => repo.GetEventRegistrationsAsync(It.IsAny<Core.Domain.Entities.Event.Event>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Registration>
                {
                    Registration.CreateRegistration(Guid.NewGuid(), "Test Name",PhoneNumber.CreateIfNotEmpty("09127926125"),@event,Email.CreateIfNotEmpty("test@gmail.com")),
                    Registration.CreateRegistration(Guid.NewGuid(), "Test Name2",PhoneNumber.CreateIfNotEmpty("09127926120"),@event,Email.CreateIfNotEmpty("test2@gmail.com")),
                });

            // Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(@event.Name, result.Result.EventName);
            Assert.NotEmpty(result.Result.EventRegistrations);
        }

        [Fact]
        public async Task Handle_ReturnsNotFoundResult_WhenRepositoryReturnsNullEvent()
        {
            // Arrange
            _eventRepositoryMock.Setup(repo => repo.GetEventByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Core.Domain.Entities.Event.Event)null);

            // Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Event does not exist", result.ErrorMessage);
        }

        [Fact]
        public async Task Handle_ReturnsFailureResult_WhenRepositoryThrowsException()
        {
            // Arrange
            _eventRepositoryMock.Setup(repo => repo.GetEventByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Test exception", result.ErrorMessage);
        }
    }
}
