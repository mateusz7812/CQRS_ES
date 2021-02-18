using ReadDB;

namespace FunctionalTests
{
    class SqLiteCtxFactoryMethod : IDbContextFactoryMethod<SqLiteDbContext>
    {
        public SqLiteDbContext Create()
        {
            return new SqLiteDbContext();
        }
    }
}