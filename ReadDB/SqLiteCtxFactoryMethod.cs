namespace ReadDB
{
    public class SqLiteCtxFactoryMethod : IDbContextFactoryMethod<SqLiteDbContext>
    {
        public SqLiteDbContext Create()
        {
            return new SqLiteDbContext();
        }
    }
}