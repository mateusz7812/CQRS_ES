using System.Data.Entity;

namespace ReadDB
{
    public interface IDbContextFactoryMethod<out T> where T:DbContext
    {
        public T Create();
    }
}
