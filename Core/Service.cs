using System;
using System.Collections.Generic;
using System.Linq;
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
            try
            {
                return _modelRepository.FindById(itemGuid);
            }
            catch (Exception e)
            {
                return Codes.DbError(e.Message);
            }
        }

        public void Delete(Guid itemGuid)
        {
            throw new NotImplementedException();
        }

        public List<T> FindAll()
        {
            return _modelRepository.FindAll();
        }

        public List<T> FindAll(Func<T, bool> func)
        {
            return FindAll().Where(func).ToList();
        }
    }
}
