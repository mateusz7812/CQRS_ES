using System;
using System.Collections.Generic;
using System.Linq;
using AccountModule.CreateAccount;
using AccountModule.Read;
using Core;
using Xunit;

namespace ModulesTests.AccountModule
{
    public class CreateAccountEventHandlerTests
    {
        class ServiceMock : IService<AccountModel>
        {
            

            public static List<AccountModel> Saved { get; } = new List<AccountModel>();

            public void Save(AccountModel model)
            {
                Saved.Add(model);
            }

            public AccountModel FindById(Guid itemGuid)
            {
                throw new NotImplementedException();
            }

            public void Delete(Guid itemGuid)
            {
                throw new NotImplementedException();
            }
        }
        [Fact]
        public void Test1()
        {
            var accountGuid = Guid.NewGuid();
            var createAccountEvent = new CreateAccountEvent(Guid.NewGuid(), accountGuid);
            var accountServiceMock = new ServiceMock();
            var eventHandler = new CreateAccountEventHandler(accountServiceMock);

            eventHandler.Handle(createAccountEvent);

            Assert.Single(ServiceMock.Saved);
            Assert.True(ServiceMock.Saved.First().Guid.Equals(accountGuid));
        }
    }
}
