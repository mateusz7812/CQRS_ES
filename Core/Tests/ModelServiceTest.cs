using System;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace Core.Tests
{
    public class ModelServiceTest
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
                .Returns((Guid guid) => list.Find(model => model.Guid.Equals(guid)));

            var service = new Service<IModel>(repositoryMock.Object);

            service.Save(modelMock.Object);
            var foundAccount = service.FindById(modelId);

            Assert.True(foundAccount.Guid.Equals(modelId));
        }

    }
}
