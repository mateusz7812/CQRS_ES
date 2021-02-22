using System;
using Optionals;

namespace Core
{
    public interface IService<T> where T : IModel
    {
        void Save(T model);
        Optional<T> FindById(Guid itemGuid);
        void Delete(Guid itemGuid);

    }
}
