using System;
using Core;

namespace TransferModule.Transfer
{
    public class TransferCommandHandler: IHandler<ICommand>
    {
        public void Handle(ICommand item)
        {
            throw new NotImplementedException();
        }

        public bool CanHandle(ICommand item)
        {
            throw new NotImplementedException();
        }
    }
}
