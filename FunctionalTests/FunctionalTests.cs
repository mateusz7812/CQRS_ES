using System;
using System.Linq;
using Applications;
using Autofac;
using Commands;
using Currencies;
using Models;
using Xunit;

namespace Tests
{
    [CollectionDefinition("FunctionalTest", DisableParallelization = true)]
    public class FunctionalTestDefinitionClass
    {
    }

    [Collection("FunctionalTest")]
    public class FunctionalTests: IClassFixture<ApplicationFixture>
    {
        [Fact]
        public void UserCreateTest()
        {
            ApplicationFixture _fixture = new();
            var app = _fixture.App;

            const string accountName = "TestName";
            var accountCreateCommand = new CreateAccountCommand {Name = accountName};

            app.HandleCommand(accountCreateCommand);
            
            var allAccounts = app.AccountModelService.FindAll();
            Assert.Single(allAccounts);
            var account = allAccounts.First();
            Assert.NotEqual(Guid.Empty, account.Guid);
            Assert.Equal(accountName, account.Name);
            Assert.Empty(account.Deposits);

            _fixture.Dispose();
        }

        [Fact]
        public void CreateDepositTest()
        {
            ApplicationFixture _fixture = new();
            var app = _fixture.App;

            app.HandleCommand(new CreateAccountCommand { Name = "TestName" });
            var accountId = app.AccountModelService.FindAll().First().Guid;

            app.HandleCommand(new CreateDepositCommand { AccountId = accountId });

            var allDeposits = app.DepositModelService.FindAll();
            Assert.Single(allDeposits);
            var depositModel = allDeposits.First();
            Assert.Equal(accountId, depositModel.Account.Guid);
            var depositId = depositModel.Guid;
            AccountModel accountModel = app.AccountModelService.FindById(accountId);
            var deposits = accountModel.Deposits;
            Assert.Single(deposits);
            Assert.Equal(depositId, deposits.First().Guid);

            _fixture.Dispose();
        }

        [Fact]
        public void TransferMoneyTest()
        {
            ApplicationFixture _fixture = new();
            var app = _fixture.App;

            var firstName = "first";
            app.HandleCommand(new CreateAccountCommand { Name = firstName });
            var firstAccountGuid = app.AccountModelService.FindAll(model => model.Name == firstName).First().Guid;
            app.HandleCommand(new CreateDepositCommand { AccountId = firstAccountGuid });

            var secondName = "second";
            app.HandleCommand(new CreateAccountCommand { Name = secondName });
            var secondAccountGuid = app.AccountModelService.FindAll(model => model.Name == secondName).First().Guid;
            app.HandleCommand(new CreateDepositCommand { AccountId = secondAccountGuid });

            var firstDepositId = app.AccountModelService.FindById(firstAccountGuid).Item.Deposits.First().Guid;
            var dollarToTransferOnDepositByAtm = Currencies.Dollars.Of(100.10);

            app.HandleCommand(new CreateAtmCommand());
            var atmId = app.AtmModelService.FindAll().First().Guid;

            app.HandleCommand(new TransferOnDepositByAtmCommand{AtmId = atmId, DepositId = firstDepositId, Currency = dollarToTransferOnDepositByAtm});
            DepositModel firstDepositToCheckPayByAtm = app.DepositModelService.FindById(firstDepositId);
            Assert.Equal(dollarToTransferOnDepositByAtm.CurrencyValue, firstDepositToCheckPayByAtm.CurrencyValue);
            Assert.Equal(dollarToTransferOnDepositByAtm.CurrencyType, firstDepositToCheckPayByAtm.CurrencyType);

            var dollarToTransferOnDepositFromDeposit = Currencies.Dollars.Of(20.02);
            var secondDepositId = app.AccountModelService.FindById(secondAccountGuid).Item.Deposits.First().Guid;
            app.HandleCommand(new TransferOnDepositFromDepositCommand{SourceDepositId = firstDepositId, DestinationDepositId = secondDepositId, Currency = dollarToTransferOnDepositFromDeposit});

            DepositModel firstDepositToTestPayFromDeposit = app.DepositModelService.FindById(firstDepositId);
            DepositModel secondDepositToTestPayFromDeposit = app.DepositModelService.FindById(secondDepositId);

            Assert.Equal(dollarToTransferOnDepositByAtm.CurrencyValue - dollarToTransferOnDepositFromDeposit.CurrencyValue, firstDepositToTestPayFromDeposit.CurrencyValue);
            Assert.Equal(CurrenciesEnum.USD, firstDepositToTestPayFromDeposit.CurrencyType);
            Assert.Equal(dollarToTransferOnDepositFromDeposit.CurrencyValue, secondDepositToTestPayFromDeposit.CurrencyValue);
            Assert.Equal(dollarToTransferOnDepositFromDeposit.CurrencyType, secondDepositToTestPayFromDeposit.CurrencyType);

            _fixture.Dispose();
        }
    }

    public class ApplicationFixture: IDisposable
    {
        public ITestApplication App { get; }
        
        private readonly ILifetimeScope _scope;

        public ApplicationFixture()
        {
            var container = ContainerConfig.Configure();
            _scope = container.BeginLifetimeScope();
            App = _scope.Resolve<ITestApplication>();
        }

        public void Dispose()
        {
            App.Dispose();
            _scope.Dispose();
        }

    }
}