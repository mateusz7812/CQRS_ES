using System;
using Commands;
using Core;
using Models;

namespace Applications
{
    public interface ITestApplication : IApplication, IDisposable
    {
        IBus<ICommand> CommandBus { get; set; }
        IBus<IEvent> EventBus { get; set; }
        IService<AccountModel> AccountModelService { get; set; }
        IService<DepositModel> DepositModelService { get; set; }
        IService<AtmModel> AtmModelService { get; set; }
        void HandleCommand(ICommand command);
    }
}