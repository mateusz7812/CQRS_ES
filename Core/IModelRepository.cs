using System;
using System.Collections.Generic;
using Optionals;

namespace Core
{
    public interface IModelRepository<T> where T : IModel
    {
        void Save(T item);
        Optional<T> FindById(Guid itemGuid);
        List<T> FindAll();
        void Delete(Guid guid);
    }
}
