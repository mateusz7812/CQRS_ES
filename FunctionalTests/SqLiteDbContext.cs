using System.Data.Entity;
using ReadDB;
using SQLite.CodeFirst;

namespace FunctionalTests
{
    class SqLiteDbContext : ModelDbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var model = modelBuilder.Build(Database.Connection);
            ISqlGenerator sqlGenerator = new SqliteSqlGenerator();
            string sql = sqlGenerator.Generate(model.StoreModel);
        }
    }
}