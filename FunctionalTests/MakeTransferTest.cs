using ReadDB;
using Xunit;

namespace Tests
{
    [Collection("DbTest")]
    public class MakeTransferTest
    {
        [Fact]
        public void Test()
        {

            //EventHandlers
            var ctxFactoryMethod = new SqLiteCtxFactoryMethod();
            ctxFactoryMethod.Create().Database.Delete();






            ctxFactoryMethod.Create().Database.Delete();
        }
    }
}