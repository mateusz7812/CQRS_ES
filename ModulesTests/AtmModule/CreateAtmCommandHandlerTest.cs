using System;
using AtmModule;
using AtmModule.CreateAtm;
using Commands;
using Core;
using Events;
using Moq;
using Xunit;

namespace ModulesTests.AtmModule
{
    public class CreateAtmCommandHandlerTest
    {
        [Fact]
        public void CreateAtmTest()
        {
            var atmServiceMock = new Mock<IAggregateService<AtmAggregate>>();
            var atmGuid = Guid.Empty;
            atmServiceMock.Setup(m => m.Load(It.IsAny<Guid>())).Returns((Guid guid) =>
            {
                atmGuid = guid;   
                return new AtmAggregate();
            });
            var eventPublisherMock = new Mock<IEventPublisher>();
            eventPublisherMock.Setup(m => m.Publish(
                It.IsAny<CreateAtmEvent>())).Callback((IEvent e)
                =>
            {
                Assert.IsType<CreateAtmEvent>(e);
                Assert.NotEqual(Guid.Empty, atmGuid);
                Assert.Equal(atmGuid, e.ItemGuid);
                Assert.Equal(Guid.Empty, e.EventGuid);
            });
            var commandHandler = new CreateAtmCommandHandler(atmServiceMock.Object, eventPublisherMock.Object);
            var command = new CreateAtmCommand();

            commandHandler.Handle(command);

            eventPublisherMock.VerifyAll();
        }

        [Fact]
        public void CreateAtmTestTakenGuid()
        {
            var atmServiceMock = new Mock<IAggregateService<AtmAggregate>>();

            var counter = 0;
            var atmGuid = Guid.Empty;
            atmServiceMock.Setup(m => m.Load(It.IsAny<Guid>())).Returns((Guid guid) =>
            {
                var aggregate = new AtmAggregate();
                if (counter > 3)
                {
                    atmGuid = guid;
                    return aggregate;
                }
                counter++;
                aggregate.Apply(new CreateAtmEvent{ItemGuid = guid});
                return aggregate;
            });
            var eventPublisherMock = new Mock<IEventPublisher>();
            eventPublisherMock.Setup(m => m.Publish(
                It.IsAny<CreateAtmEvent>())).Callback((IEvent e)
                    =>
            {
                Assert.IsType<CreateAtmEvent>(e);
                Assert.NotEqual(Guid.Empty, atmGuid);
                Assert.Equal(atmGuid, e.ItemGuid);
                Assert.Equal(Guid.Empty, e.EventGuid);
            });
            var commandHandler = new CreateAtmCommandHandler(atmServiceMock.Object, eventPublisherMock.Object);
            var command = new CreateAtmCommand();

            commandHandler.Handle(command);

            eventPublisherMock.VerifyAll();
        }
    }
}