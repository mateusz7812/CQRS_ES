using Core;
using Events;

namespace DepositModule.CreateDeposit
{
    public class AddDepositToAccountEventHandler: IEventHandler<AddDepositToAccountEvent>
    {
        public void Handle(IEvent item)
        {
            // things to do after deposit creating
        }

        public bool CanHandle(IEvent item) => item is AddDepositToAccountEvent;
    }
}