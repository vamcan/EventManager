using EventManager.Core.Application.Event.GetAllEvents;
using EventManager.Core.Domain.Contracts.Repository;
using EventManager.Core.Domain.Entities.User;
using EventManager.Core.Domain.ValueObjects;
using Moq;
using Xunit;

namespace EventManager.UnitTests.Handlers.Event
{
    public class GetAllEventsHandlerTests
    {
        private readonly Mock<IEventRepository> _eventRepositoryMock;
        private readonly GetAllEventsHandler _handler;
        private readonly GetAllEventsQuery _request;

        public GetAllEventsHandlerTests()
        {
            _eventRepositoryMock = new Mock<IEventRepository>();
            _handler = new GetAllEventsHandler(_eventRepositoryMock.Object);
            _request = new GetAllEventsQuery();
        }

        [Fact]
        public async Task Handle_ReturnsSuccessResult_WhenRepositoryReturnsEvents()
        {
            // Arrange
            var userName = "TestUserName";
            var user = User.CreateUser(Guid.NewGuid(), "test user", "TestUserName", userName, Email.CreateIfNotEmpty("test@gmail.com"));
            _eventRepositoryMock.Setup(repo => repo.GetAllEventsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Core.Domain.Entities.Event.Event> {
                   
                    Core.Domain.Entities.Event.Event.CreatEvent(Guid.NewGuid(), "Test Event", "This is a test event", "Test location",
                    DateTime.Now, DateTime.Now.AddHours(1), user),
                    Core.Domain.Entities.Event.Event.CreatEvent(Guid.NewGuid(), "Test Event2", "This is a test event2", "Test location2",
                        DateTime.Now, DateTime.Now.AddHours(1), user),
                });

            // Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Result.GetEventResults);
        }

        [Fact]
        public async Task Handle_ReturnsFailureResult_WhenRepositoryThrowsException()
        {
            // Arrange
            _eventRepositoryMock.Setup(repo => repo.GetAllEventsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _handler.Handle(_request, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Test exception", result.ErrorMessage);
        }
    }
}
