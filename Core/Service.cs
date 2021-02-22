using System;
using Optionals;

namespace Core
{
    public class Service<T> : IService<T> where T : IModel
    {
        private readonly IModelRepository<T> _modelRepository;

        public Service(IModelRepository<T> modelRepository)
        {
            _modelRepository = modelRepository;
        }

        public void Save(T model)
        {
            _modelRepository.Save(model);
        }

        public Optional<T> FindById(Guid itemGuid)
        {
            return _modelRepository.FindById(itemGuid);
        }

        public void Delete(Guid itemGuid)
        {
            throw new NotImplementedException();
        }
    }
}
