using System;

namespace Core
{
    public interface IService<T> where T : IModel
    {
        void Save(T model);
        T FindById(Guid itemGuid);
        void Delete(Guid itemGuid);

    }
}
