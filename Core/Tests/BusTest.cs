using Moq;
using Xunit;

namespace Core.Tests
{
    public class BusTest
    {
        [Fact]
        public void TestBasic()
        {
            var handleableMock = new Mock<IHandleable>().Object;
            HandlerFactoryMethod<IHandleable> factoryMethod = new HandlerFactoryMethod<IHandleable>();
            ICache<IHandleable> cache = new Cache<IHandleable>();
            var commandBus = new Bus<IHandleable>(factoryMethod, cache);
            var commandHandlerMock = new Mock<IHandler<IHandleable>>();
            commandHandlerMock.Setup(m => m.CanHandle(It.IsAny<IHandleable>())).Returns(true);
            commandHandlerMock.Setup(m => m.Handle(It.Is<IHandleable>(h => h == handleableMock)));
            factoryMethod.AddHandler(commandHandlerMock.Object);

            commandBus.Add(handleableMock);
            Assert.False(commandBus.IsBusEmpty);
            commandBus.HandleNext();

            commandHandlerMock.VerifyAll();
        }
    }
}
