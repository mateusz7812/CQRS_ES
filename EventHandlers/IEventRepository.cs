using System;
using System.Collections.Generic;
using System.Text;
using EventHandlers.Models;

namespace EventHandlers
{
    public interface IEventRepository<T> where T: IModel
    {
        void Save(T item);
        T FindById(Guid itemGuid);
        List<T> FindAll();
    }
}
