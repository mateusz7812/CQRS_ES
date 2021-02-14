using System;

namespace Core
{
    public abstract class Service<T> : IService<T> where T : IModel
    {
        private readonly IRepository<T> _repository;

        protected Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual void Save(T model)
        {
            _repository.Save(model);
        }

        public virtual T FindById(Guid itemGuid)
        {
            return _repository.FindById(itemGuid);
        }

        public virtual void Delete(Guid itemGuid)
        {
            throw new NotImplementedException();
        }
    }
}
