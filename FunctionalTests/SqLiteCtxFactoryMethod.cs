using ReadDB;

namespace Tests
{
    class SqLiteCtxFactoryMethod : IDbContextFactoryMethod<SqLiteDbContext>
    {
        public SqLiteDbContext Create()
        {
            return new SqLiteDbContext();
        }
    }
}