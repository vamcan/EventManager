using EventManager.Core.Application.Event.GetEvent;
using EventManager.Core.Domain.Contracts.Repository;
using EventManager.Core.Domain.ValueObjects;
using Moq;
using Xunit;

namespace EventManager.UnitTests.Handlers.Event
{
    public class GetEventHandlerTests
    {
        private readonly Mock<IEventRepository> _eventRepositoryMock;
        private readonly GetEventHandler _handler;
        private readonly GetEventQuery _request;

        public GetEventHandlerTests()
        {
            _eventRepositoryMock = new Mock<IEventRepository>();
            _handler = new GetEventHandler(_eventRepositoryMock.Object);
            _request = new GetEventQuery { EventId = Guid.NewGuid() };
        }

        [Fact]
        public async Task Handle_ReturnsSuccessResult_WhenRepositoryReturnsEvent()
        {
            // Arrange
            var userName = "TestUserName";
            var user = Core.Domain.Entities.User.User.CreateUser(Guid.NewGuid(), "test user", "TestUserName", userName, Email.CreateIfNotEmpty("test@gmail.com"));

            _eventRepositoryMock.Setup(repo => repo.GetEventByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Core.Domain.Entities.Event.Event.CreatEvent(Guid.NewGuid(), "Test Event", "This is a test event", "Test location",
                    DateTime.Now, DateTime.Now.AddHours(1), user));

            // Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
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
