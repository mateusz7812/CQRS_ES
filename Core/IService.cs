using System;
using System.Collections;
using System.Collections.Generic;
using Optionals;

namespace Core
{
    public interface IService<T> where T : IModel
    {
        void Save(T model);
        Optional<T> FindById(Guid itemGuid);
        void Delete(Guid itemGuid);
        public List<T> FindAll();
        public List<T> FindAll(Func<T, bool> func);
    }
}
