using EventManager.Core.Application.Event.AddEvent;
using EventManager.Core.Domain.Contracts.Repository;
using EventManager.Core.Domain.Entities.User;
using Moq;
using Xunit;

namespace EventManager.UnitTests.Handlers.Event
{
    public class AddEventHandlerUnitTests
    {
        private readonly Mock<IEventRepository> _mockEventRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly AddEventHandler _addEventHandler;

        public AddEventHandlerUnitTests()
        {
            _mockEventRepository = new Mock<IEventRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _addEventHandler = new AddEventHandler(_mockEventRepository.Object, _mockUserRepository.Object);
        }

        [Fact]
        public async Task TestAddEventHandler()
        {
            var userId = Guid.NewGuid();
            var user = User.CreateUser(userId, "test user", "TestUserName", "TestPass","test@gmail.com", "e10adc3949ba59abbe56e057f20f883e");
            _mockUserRepository.Setup(repo => repo.GetUserByIdAsync(userId))
                .ReturnsAsync(user);

            var eventId = Guid.NewGuid();
            var @event = Core.Domain.Entities.Event.Event.CreatEvent(eventId, "Test Event", "This is a test event", "Test location",
                DateTime.Now, DateTime.Now.AddHours(1), user);
            _mockEventRepository
                .Setup(repo =>
                    repo.AddEventAsync(It.IsAny< Core.Domain.Entities.Event.Event >(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(@event);

            var request = new AddEventCommand
            {
                UserId = userId,
                Name = @event.Name,
                Description = @event.Description,
                Location = @event.Location,
                StartTime = @event.StartTime,
                EndTime = @event.EndTime,
            };

            // Act
            var result = await _addEventHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(eventId, result.Result.Id);
            Assert.Equal(request.Name, result.Result.Name);
            Assert.Equal(request.Description, result.Result.Description);
            Assert.Equal(request.Location, result.Result.Location);
            Assert.Equal(request.StartTime, result.Result.StartTime);
            Assert.Equal(request.EndTime, result.Result.EndTime);
        }


        [Fact]
        public async Task AddEventHandler_Should_Not_Add_Event_If_User_Does_Not_Exist()
        {
            var userId =Guid.NewGuid();
            _mockUserRepository.Setup(r => r.GetUserByIdAsync(userId))
                .Returns(Task.FromResult<User>(null));

            var command = new AddEventCommand()
            {
                UserId = userId,
                Name = "Test Event",
                Description = "Test event description",
                Location = "Test location",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1)
            };

            // Act
            var result = await _addEventHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("User does not exist", result.ErrorMessage);
        }

        [Fact]
        public async Task AddEventHandler_Should_Not_Add_Event_If_IEventRepository_Throws_An_Exception()
        {

            var userId = Guid.NewGuid();
            var user = User.CreateUser(userId, "test user", "TestUserName", "TestPass","test@test.com", "e10adc3949ba59abbe56e057f20f883e");
            _mockUserRepository.Setup(repo => repo.GetUserByIdAsync(userId))
                .ReturnsAsync(user);

            _mockEventRepository
                .Setup(repo =>
                    repo.AddEventAsync(It.IsAny<Core.Domain.Entities.Event.Event>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test exception"));

            var command = new AddEventCommand()
            {
                UserId = userId,
                Name = "Test Event",
                Description = "Test event description",
                Location = "Test location",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1)
            };
            
            // Act
            var result = await _addEventHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
        }
    }
}
