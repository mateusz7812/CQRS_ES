using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Models;
using Moq;
using Optionals;
using ReadDB;
using Xunit;

namespace FunctionalTests
{
    public class DepositModelServiceTest
    {
        private static Mock<IModelRepository<DepositModel>> DepositRepositoryMock()
        {
            var mock = new Mock<IModelRepository<DepositModel>>();
            var list = new List<DepositModel>();
            mock.Setup(m => m.Save(It.IsAny<DepositModel>())).Callback((DepositModel it) => list.Add(it));
            mock.Setup(m => m.FindById(It.IsAny<Guid>()))
                .Returns((Guid guid) =>
                {
                    return list.Any(m => m.Guid == guid)
                        ? list.FirstOrDefault(model => model.Guid.Equals(guid))
                        : Codes.NotFound;
                });
            mock.Setup(m => m.Delete(It.IsAny<Guid>()))
                .Callback((Guid guid) => list.RemoveAll(deposit => deposit.Guid.Equals(guid)));
            return mock;
        }

        [Fact]
        public void TestCreateNewDepositModel()
        {
            var depositGuid = Guid.NewGuid();
            var accountGuid = Guid.NewGuid();
            var tempRepositoryMock = DepositRepositoryMock();
            var dbRepositoryMock = DepositRepositoryMock();
            dbRepositoryMock.Setup(m => m.Save(It.Is<DepositModel>(d => d.Account == default)))
                .Throws(new Exception("Account is null"));
            var service = new DepositService(tempRepositoryMock.Object, dbRepositoryMock.Object);
            var depositModel = new DepositModel {Guid = depositGuid};
            service.Save(depositModel);

            DepositModel saved = service.FindById(depositGuid);
            Assert.Same(depositModel, saved);

            depositModel.Account = new AccountModel {Guid = accountGuid};
            service.Save(depositModel);
            
            saved = service.FindById(depositGuid);
            Assert.Same(depositModel, saved);
        }
        
        [Fact]
        public void TestFindNotExistingDeposit()
        {
            var depositGuid = Guid.NewGuid();
            var tempRepositoryMock = DepositRepositoryMock();
            var dbRepositoryMock = DepositRepositoryMock();
            var service = new DepositService(tempRepositoryMock.Object, dbRepositoryMock.Object);

            var depositOptional = service.FindById(depositGuid);

            tempRepositoryMock.Verify(m => m.FindById(depositGuid));
            tempRepositoryMock.VerifyNoOtherCalls();
            dbRepositoryMock.Verify(m => m.FindById(depositGuid));
            dbRepositoryMock.VerifyNoOtherCalls();

            Assert.Same(Codes.NotFound, depositOptional.Code);
            Assert.Throws<Exception>(() => depositOptional.Item);
        }
    }
}