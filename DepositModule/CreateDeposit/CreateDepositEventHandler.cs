using Core;
using Events;
using Models;

namespace DepositModule.CreateDeposit
{
    public class CreateDepositEventHandler: AbstractEventHandler<CreateDepositEvent>
    {
        private readonly IService<DepositModel> _depositService;

        public CreateDepositEventHandler(IService<DepositModel> depositService)
        {
            _depositService = depositService;
        }

        public override void Handle(IEvent item)
        {
            var createDepositEvent = (CreateDepositEvent) item;
            var deposit = new DepositModel
            {
                Guid = createDepositEvent.ItemGuid,
                Account = new AccountModel { Guid = createDepositEvent.AccountGuid }
            };
            _depositService.Save(deposit);
        }

    }
}
