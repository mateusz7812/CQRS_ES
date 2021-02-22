using System;
using System.Collections.Generic;
using Core;
using Moq;
using Optionals;
using Xunit;

namespace FunctionalTests
{
    public class AccountModelServiceTest
    {
        [Fact]
        public void CreateAccountTest()
        {
            var modelId = Guid.NewGuid();
            var modelMock = new Mock<IModel>();
            modelMock.Setup(m => m.Guid).Returns(modelId);
            var repositoryMock = new Mock<IModelRepository<IModel>>();

            var list = new List<IModel>();
            repositoryMock.Setup(m => m.Save(It.IsAny<IModel>())).Callback((IModel it) => list.Add(it));
            repositoryMock.Setup(m => m.FindById(It.IsAny<Guid>()))
                .Returns((Guid guid) => new Optional<IModel>
                {
                    Item = list.Find(model => model.Guid.Equals(guid)),
                    Code = Codes.Success
                });

            var service = new Service<IModel>(repositoryMock.Object);
            
            service.Save(modelMock.Object);
            var accountOptional = service.FindById(modelId);

            Assert.Same(accountOptional.Code, Codes.Success);
            Assert.True(accountOptional.Item.Guid.Equals(modelId));
        }

        [Fact]
        public void AccountNotFoundTest()
        {
            var modelId = Guid.NewGuid();
            var repositoryMock = new Mock<IModelRepository<IModel>>();
            
            repositoryMock.Setup(m => m.FindById(It.IsAny<Guid>()))
                .Returns((Guid guid) => new Optional<IModel>
                {
                    Item = null,
                    Code = Codes.NotFound
                });

            var service = new Service<IModel>(repositoryMock.Object);

            var accountOptional = service.FindById(modelId);

            Assert.Same(accountOptional.Code, Codes.NotFound);
        }

    }
}
