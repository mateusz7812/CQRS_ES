using System;
using System.Collections.Generic;
using System.Linq;
using AccountModule.CreateAccount;
using Core;
using Events;
using Models;
using Optionals;
using Xunit;

namespace ModulesTests.AccountModule
{
    public class CreateAccountEventHandlerTests
    {
        class ServiceMock : IService<AccountModel>
        {
            public static List<AccountModel> Saved { get; } = new List<AccountModel>();
            public void Save(AccountModel model) => Saved.Add(model);
            public Optional<AccountModel> FindById(Guid itemGuid) => throw new NotImplementedException();
            public void Delete(Guid itemGuid) => throw new NotImplementedException();
        }
        [Fact]
        public void Test1()
        {
            var accountGuid = Guid.NewGuid();
            var accountName = "testName";
            var createAccountEvent = new CreateAccountEvent{
                EventGuid = Guid.NewGuid(), 
                ItemGuid = accountGuid, 
                AccountName = accountName

            };
            var accountServiceMock = new ServiceMock();
            var eventHandler = new CreateAccountEventHandler(accountServiceMock);

            eventHandler.Handle(createAccountEvent);

            Assert.Single(ServiceMock.Saved);
            var accountModel = ServiceMock.Saved.First();
            Assert.True(accountModel.Guid.Equals(accountGuid));
            Assert.True(accountModel.Name.Equals(accountName));
        }
    }
}
