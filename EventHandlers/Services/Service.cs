using System;
using EventHandlers.Models;

namespace EventHandlers.Services
{
    public abstract class Service<T>: IService<T> where T: IModel
    {
        private readonly IRepository<T> _repository;

        protected Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual void Save(T model)
        {
            throw new NotImplementedException();
        }

        public virtual T FindById(Guid itemGuid)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(Guid itemGuid)
        {
            throw new NotImplementedException();
        }
    }
}
