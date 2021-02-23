using Core;
using Events;

namespace DepositModule.CreateDeposit
{
    public class AddDepositToAccountEventHandler: IHandler<IEvent>
    {
        public void Handle(IEvent item)
        {
            throw new System.NotImplementedException();
        }

        public bool CanHandle(IEvent item) => item is AddDepositToAccountEvent;
    }
}