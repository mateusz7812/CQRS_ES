using System;
using AccountModule.CreateAccount;
using AccountModule.Read;
using Moq;
using Xunit;

namespace AccountModule.Tests
{
    public class CreateAccountEventHandlerTests
    {
        [Fact]
        public void Test1()
        {
            var accountGuid = Guid.NewGuid();
            var createAccountEvent = new CreateAccountEvent(Guid.NewGuid(), accountGuid);
            var accountServiceMock = new Mock<AccountService>(new object[] { null });
            accountServiceMock.Setup(s => s.Save(It.Is<AccountModel>(a => a.Guid.Equals(accountGuid))));
            var eventHandler = new CreateAccountEventHandler(accountServiceMock.Object);

            eventHandler.Handle(createAccountEvent);

            accountServiceMock.VerifyAll();
        }
    }
}
