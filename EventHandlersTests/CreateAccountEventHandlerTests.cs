using System;
using System.Xml.Linq;
using EventHandlers;
using EventHandlers.EventHandlers;
using EventHandlers.Models;
using EventHandlers.Services;
using EventsAndCommands.Events;
using Moq;
using Xunit;

namespace EventHandlersTests
{
    public class CreateAccountEventHandlerTests
    {
        [Fact]
        public void Test1()
        {
            var accountGuid = Guid.NewGuid();
            var createAccountEvent = new CreateAccountEvent(Guid.NewGuid(), accountGuid);
            var accountServiceMock = new Mock<AccountService>(new object[]{null});
            accountServiceMock.Setup(s => s.Save(It.Is<Account>(a => a.Guid.Equals(accountGuid))));
            var eventHandler = new CreateAccountEventHandler(accountServiceMock.Object);
            
            eventHandler.Handle(createAccountEvent);

            accountServiceMock.VerifyAll();
        }
    }
}
