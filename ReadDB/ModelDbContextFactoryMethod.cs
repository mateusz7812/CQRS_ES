namespace ReadDB
{
    class ModelDbContextFactoryMethod : IDbContextFactoryMethod<ModelDbContext>
    {
        public ModelDbContext Create()
        {
            return new ModelDbContext();
        }
    }
}
