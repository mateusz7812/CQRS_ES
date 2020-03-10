using System.Data.SQLite;

namespace EventHandlers.Repositories

{
    public class SqlLiteRepository: IRepository
    {
        private readonly string _url;

        public SqlLiteRepository(string url)
        {
            _url = url;
        }

        private void OpenConnection()
        {
            using (var con = new SQLiteConnection(_url))
            {
                con.Open();
            }
        }

        private SQLiteDataReader ExecuteCommand(string command, SQLiteConnection connection)
        {
            using (var cmd = new SQLiteCommand(connection))
            {
                cmd.CommandText = command;
                cmd.ExecuteNonQuery();
                return cmd.ExecuteReader();
            }
        }
    }
}
