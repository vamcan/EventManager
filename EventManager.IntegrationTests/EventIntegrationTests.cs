using EventManager.Core.Application.Event.AddEvent;
using EventManager.Core.Application.Event.GetAllEvents;
using EventManager.Core.Application.Event.GetEvent;
using EventManager.Core.Domain.Entities.User;
using EventManager.Core.Domain.ValueObjects;
using EventManager.IntegrationTests.Base;

namespace EventManager.IntegrationTests
{
    public class EventIntegrationTests : IClassFixture<BaseRepositoryFixture>
    {
        private readonly User _user;
        private readonly BaseRepositoryFixture _factory;
        public EventIntegrationTests(BaseRepositoryFixture factory)
        {
            _factory = factory;

            var userName = "TestUserName";
            _user = User.CreateUser(Guid.NewGuid(), "test user", "TestUserName", userName, Email.CreateIfNotEmpty("test@gmail.com"));
            _user.SetPasswordHash("TestPassword");

        }

        [Fact]
        public async Task AddEvent_WithValidInput_ReturnsSuccessResult()
        {
            // Arrange
            _factory.Build();
            var handler = new AddEventHandler(_factory.EventRepository, _factory.UserRepository);
            var command = new AddEventCommand
            {
                UserName = _user.UserName,
                Name = "Test Event",
                Description = "This is a test event",
                Location = "Test location",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1)
            };

            await _factory.UserRepository.AddUserAsync(_user, CancellationToken.None);
            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(command.Name, result.Result.Name);
            Assert.Equal(command.Description, result.Result.Description);
            Assert.Equal(command.Location, result.Result.Location);
            Assert.Equal(command.StartTime, result.Result.StartTime);
            Assert.Equal(command.EndTime, result.Result.EndTime);
        }

        [Fact]
        public async Task GetAllEvents_WithValidInput_ReturnsSuccessResult()
        {
            // Arrange
            _factory.Build();
            var addEventHandler = new AddEventHandler(_factory.EventRepository, _factory.UserRepository);
            var getAllEventsHandler = new GetAllEventsHandler(_factory.EventRepository);
            var event1 = new AddEventCommand
            {
                UserName = _user.UserName,
                Name = "Test Event",
                Description = "This is a test event",
                Location = "Test location",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1)
            };
            var event2 = new AddEventCommand
            {
                UserName = _user.UserName,
                Name = "Test Event2",
                Description = "This is a test event2",
                Location = "Test location2",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2)
            };
            await _factory.UserRepository.AddUserAsync(_user, CancellationToken.None);
            await addEventHandler.Handle(event1, CancellationToken.None);
            await addEventHandler.Handle(event2, CancellationToken.None);
            var request = new GetAllEventsQuery();

           
            // Act
            var result = await getAllEventsHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Result.GetEventResults);
            Assert.Equal(2,result.Result.GetEventResults.Count);
        }

        [Fact]
        public async Task GetEvent_WithValidInput_ReturnsSuccessResult()
        {
            // Arrange
            _factory.Build();
            var addEventHandler = new AddEventHandler(_factory.EventRepository, _factory.UserRepository);
            var getAllEventsHandler = new GetAllEventsHandler(_factory.EventRepository);
            var getEventHandler = new GetEventHandler(_factory.EventRepository);
            var event1 = new AddEventCommand
            {
                UserName = _user.UserName,
                Name = "Test Event",
                Description = "This is a test event",
                Location = "Test location",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1)
            };
         
            await _factory.UserRepository.AddUserAsync(_user, CancellationToken.None);
            await addEventHandler.Handle(event1, CancellationToken.None);
            var requestGetAll = new GetAllEventsQuery();
            var resultAll = await getAllEventsHandler.Handle(requestGetAll, CancellationToken.None);
            var request = new GetEventQuery()
            {
                EventId = resultAll.Result.GetEventResults.First().Id,
            };

            // Act
            var result = await getEventHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(event1.Name, result.Result.Name);
            Assert.Equal(event1.Description, result.Result.Description);
            Assert.Equal(event1.Location, result.Result.Location);
        }

    }
}
