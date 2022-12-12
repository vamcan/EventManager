using EventManager.Core.Application.Event.RegisterAtEvent;
using EventManager.Core.Domain.Contracts.Repository;
using EventManager.Core.Domain.Entities.Event;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManager.Core.Domain.ValueObjects;
using Xunit;
using EventManager.Core.Domain.Entities.User;
using Microsoft.Extensions.Logging;

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
                PhoneNumber = PhoneNumber.CreateIfNotEmpty("123-456-7890"),
                Email = Email.CreateIfNotEmpty("reza.ghasemi@example.com")
            };
            var user = User.CreateUser(1, "test user", "TestUserName", "TestPass");
            var currentEvent = Core.Domain.Entities.Event.Event.CreatEvent(request.EventId, "Test Event", "This is a test event", "Test location",
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
            Assert.Equal(request.PhoneNumber.Value, result.Result.PhoneNumber);
            Assert.Equal(request.Email.Value, result.Result.Email); 
            Assert.Equal(currentEvent.Name, result.Result.EventName);
            Assert.Equal(currentEvent.StartTime, result.Result.EventStartTime);
            Assert.Equal(currentEvent.EndTime, result.Result.EventEndTime);

        }
    }
}