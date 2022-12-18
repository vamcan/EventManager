using EventManager.Core.Application.Event.AddEvent;
using EventManager.Core.Application.Event.GetAllEvents;
using EventManager.Core.Application.Event.GetEvent;
using EventManager.Core.Application.Event.GetEventRegistrations;
using EventManager.Core.Application.Event.RegisterInEvent;
using EventManager.Core.Domain.Entities.Event;
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

            var getAllEventsHandler = new GetAllEventsHandler(_factory.EventRepository);
            var event1 = Event.CreateEvent(Guid.NewGuid(), "Test Event1", "This is a test event1", "Test location1",
                DateTime.Now, DateTime.Now.AddHours(1), _user);
            var event2 = Event.CreateEvent(Guid.NewGuid(), "Test Event2", "This is a test event2", "Test location2",
                DateTime.Now, DateTime.Now.AddHours(1), _user);
            await _factory.EventRepository.AddEventAsync(event1, CancellationToken.None);
            await _factory.EventRepository.AddEventAsync(event2, CancellationToken.None);
            var request = new GetAllEventsQuery();
            // Act
            var result = await getAllEventsHandler.Handle(request, CancellationToken.None);
            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Result.GetEventResults);
            Assert.Equal(2, result.Result.GetEventResults.Count);
        }

        [Fact]
        public async Task GetEvent_WithValidInput_ReturnsSuccessResult()
        {
            // Arrange
            _factory.Build();
            var getEventHandler = new GetEventHandler(_factory.EventRepository);
            var eventId = Guid.NewGuid();
            var @event = Event.CreateEvent(eventId, "Test Event", "This is a test event", "Test location",
                DateTime.Now, DateTime.Now.AddHours(1), _user);

            await _factory.UserRepository.AddUserAsync(_user, CancellationToken.None);
            await _factory.EventRepository.AddEventAsync(@event, CancellationToken.None);
            var request = new GetEventQuery()
            {
                EventId = eventId
            };

            // Act
            var result = await getEventHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(@event.Name, result.Result.Name);
            Assert.Equal(@event.Description, result.Result.Description);
            Assert.Equal(@event.Location, result.Result.Location);

        }

        [Fact]
        public async void RegisterInEvent_GivenValidCommand_ShouldReturnSuccessResult()
        {
            // Arrange
            _factory.Build();

            var eventId = Guid.NewGuid();
            var @event = Event.CreateEvent(eventId, "Test Event", "This is a test event", "Test location",
                DateTime.Now, DateTime.Now.AddHours(1), _user);

            await _factory.UserRepository.AddUserAsync(_user, CancellationToken.None);
            await _factory.EventRepository.AddEventAsync(@event, CancellationToken.None);

            var handler = new RegisterInEventHandler(_factory.EventRepository);
            var command = new RegisterInEventCommand
            {
                EventId = eventId,
                Name = "Test Name",
                PhoneNumber = "1234567890",
                Email = "test@example.com"
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            var registerInEventResult = result.Result;
            Assert.Equal(command.Email, registerInEventResult.Email);
            Assert.Equal(@event.EndTime, registerInEventResult.EventEndTime);
            Assert.Equal(@event.Name, registerInEventResult.EventName);
            Assert.Equal(@event.StartTime, registerInEventResult.EventStartTime);
            Assert.Equal(command.Name, registerInEventResult.Name);
            Assert.Equal(command.PhoneNumber, registerInEventResult.PhoneNumber);
        }
        [Fact]
        public async Task GetEventRegistrations_ReturnsSuccessResult_WhenRepositoryReturnsEventAndRegistrations()
        {
            // Arrange
            _factory.Build();

            var eventId = Guid.NewGuid();
            var @event = Event.CreateEvent(eventId, "Test Event", "This is a test event", "Test location",
                DateTime.Now, DateTime.Now.AddHours(1), _user);

            await _factory.UserRepository.AddUserAsync(_user, CancellationToken.None);

            var person1 = Registration.CreateRegistration(Guid.NewGuid(), "Test Name",
                PhoneNumber.CreateIfNotEmpty("09127926125"), @event, Email.CreateIfNotEmpty("test@gmail.com"));

            var person2 = Registration.CreateRegistration(Guid.NewGuid(), "Test Name2",
                PhoneNumber.CreateIfNotEmpty("09127926120"), @event, Email.CreateIfNotEmpty("test2@gmail.com"));
            @event.RegisterInEvent(person1);
            @event.RegisterInEvent(person2);
            await _factory.EventRepository.AddEventAsync(@event, CancellationToken.None);


            var request = new EventRegistrationsQuery { EventId = eventId };
            var handler = new GetEventRegistrationsHandler(_factory.EventRepository);
            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(@event.Name, result.Result.EventName);
            Assert.NotEmpty(result.Result.EventRegistrations);
            Assert.Equal(2, result.Result.EventRegistrations.Count);
        }

    }
}
