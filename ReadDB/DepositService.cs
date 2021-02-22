using System;
using Core;
using Models;
using Optionals;

namespace ReadDB
{
    public class DepositService: IService<DepositModel>
    {
        private readonly IModelRepository<DepositModel> _tempRepository;
        private readonly IModelRepository<DepositModel> _dbRepository;

        public DepositService(IModelRepository<DepositModel> tempRepository,
            IModelRepository<DepositModel> dbRepository)
        {
            _tempRepository = tempRepository;
            _dbRepository = dbRepository;
        }

        public void Save(DepositModel deposit)
        {
            throw new NotImplementedException();
        }

        public Optional<DepositModel> FindById(Guid itemGuid)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid itemGuid)
        {
            throw new NotImplementedException();
        }
    }
}
