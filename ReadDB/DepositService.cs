using System;
using Core;
using Models;
using Optionals;

namespace ReadDB
{
    public class DepositService : IService<DepositModel>
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
            if (deposit.Account != default(AccountModel))
            {
                _dbRepository.Save(deposit);
                _tempRepository.Delete(deposit.Guid);
            }
            else
            {
                _tempRepository.Save(deposit);
            }
        }

        public Optional<DepositModel> FindById(Guid itemGuid)
        {
            var optional = _dbRepository.FindById(itemGuid);
            if (optional.Code == Codes.NotFound)
                optional = _tempRepository.FindById(itemGuid);
            return optional;
        }

        public void Delete(Guid itemGuid)
        {
            throw new NotImplementedException();
        }
    }
}