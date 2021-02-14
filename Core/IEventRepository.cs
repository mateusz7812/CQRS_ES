using System;
using System.Collections.Generic;

namespace Core
{
    public interface IRepository<T> where T : IModel
    {
        void Save(T item);
        T FindById(Guid itemGuid);
        List<T> FindAll();
    }
}
