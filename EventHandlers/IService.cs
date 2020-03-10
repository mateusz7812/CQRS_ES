using System;
using System.Collections.Generic;
using System.Text;
using EventHandlers.Models;

namespace EventHandlers
{
    public interface IService<T> where T: IModel
    {
        void Save(T model);
        T FindById(Guid itemGuid);
        void Delete(Guid itemGuid);

    }
}
