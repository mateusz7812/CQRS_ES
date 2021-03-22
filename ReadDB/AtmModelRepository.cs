using System;
using System.Collections.Generic;
using Core;
using Models;
using Optionals;

namespace ReadDB
{
    public class AtmModelRepository: IModelRepository<AtmModel>
    {
        public void Save(AtmModel item)
        {
            throw new NotImplementedException();
        }

        public Optional<AtmModel> FindById(Guid itemGuid)
        {
            throw new NotImplementedException();
        }

        public List<AtmModel> FindAll()
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}