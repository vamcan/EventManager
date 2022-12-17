using EventManager.Core.Application.Event.AddEvent;
using EventManager.Core.Domain.Contracts.Repository;
using EventManager.Core.Domain.Entities.User;
using EventManager.Core.Domain.ValueObjects;
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
            var userName = "TestUserName";
            var user = User.CreateUser(Guid.NewGuid(), "test user", "TestUserName", userName,Email.CreateIfNotEmpty("test@gmail.com"));
            _mockUserRepository.Setup(repo => repo.FindByUserNameAsync(userName,CancellationToken.None))
                .ReturnsAsync(user);

            var eventId = Guid.NewGuid();
            var @event = Core.Domain.Entities.Event.Event.CreatEvent(eventId, "Test Event", "This is a test event", "Test location",
                DateTime.Now, DateTime.Now.AddHours(1), user);
            _mockEventRepository
                .Setup(repo =>
                    repo.AddEventAsync(It.IsAny< Core.Domain.Entities.Event.Event >(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var request = new AddEventCommand
            {
                UserName = user.UserName,
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
            Assert.Equal(request.Name, result.Result.Name);
            Assert.Equal(request.Description, result.Result.Description);
            Assert.Equal(request.Location, result.Result.Location);
            Assert.Equal(request.StartTime, result.Result.StartTime);
            Assert.Equal(request.EndTime, result.Result.EndTime);
        }


        [Fact]
        public async Task AddEventHandler_Should_Not_Add_Event_If_User_Does_Not_Exist()
        {
            string userName = "InvalidUserName";
            _mockUserRepository.Setup(r => r.FindByUserNameAsync(userName, CancellationToken.None))
                .Returns(Task.FromResult<User>(null!)!);

            var command = new AddEventCommand()
            {
                UserName = userName,
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
        public async Task AddEventHandler_Should_Not_Add_Event_If_EndTime_Is_Smaller_Than_StartTime()
        {
            string userName = "TestUserName";
            var user = User.CreateUser(Guid.NewGuid(), "test user", "Test Family", userName, Email.CreateIfNotEmpty("test@test.com"));
            _mockUserRepository.Setup(repo => repo.FindByUserNameAsync(userName, CancellationToken.None))
                .ReturnsAsync(user);

            var command = new AddEventCommand()
            {
                UserName = user.UserName,
                Name = "Test Event",
                Description = "Test event description",
                Location = "Test location",
                StartTime = DateTime.Now.AddDays(1),
                EndTime = DateTime.Now
            };

            // Act
            var result = await _addEventHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("The end time cannot be smaller than the start time", result.ErrorMessage);
        }

        [Fact]
        public async Task AddEventHandler_Should_Not_Add_Event_If_IEventRepository_Throws_An_Exception()
        {

            string userName = "TestUserName";
            var user = User.CreateUser(Guid.NewGuid(), "test user","Test Family", userName, Email.CreateIfNotEmpty("test@test.com"));
            _mockUserRepository.Setup(repo => repo.FindByUserNameAsync(userName, CancellationToken.None))
                .ReturnsAsync(user);

            _mockEventRepository
                .Setup(repo =>
                    repo.AddEventAsync(It.IsAny<Core.Domain.Entities.Event.Event>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test exception"));

            var command = new AddEventCommand()
            {
                UserName = userName,
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
