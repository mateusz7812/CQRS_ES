using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Core;

namespace AccountModule.Read

{
    public class AccountSqlLiteRepository : IRepository<AccountModel>
    {
        private readonly string _tableName;
        private SQLiteConnection _connection;

        public AccountSqlLiteRepository(string tableName)
        {
            _tableName = tableName;
            OpenConnection();
        }

        private void OpenConnection()
        {
            _connection = new SQLiteConnection("Data Source=:memory:").OpenAndReturn();
        }

        private SQLiteCommand ExecuteCommand(string command)
        {
            var cmd = new SQLiteCommand(_connection) { CommandText = command };
            cmd.ExecuteNonQuery();
            return cmd;
        }

        public void CreateTable()
        {
            var command = "CREATE TABLE " + _tableName + " (id CHAR(36) PRIMARY KEY);";
            ExecuteCommand(command);
        }

        public void Save(AccountModel account)
        {
            var command = "INSERT INTO " + _tableName + " VALUES ('" + account.Guid + "')";
            ExecuteCommand(command);
        }

        public AccountModel FindById(Guid accountGuid)
        {
            var command = "SELECT * FROM " + _tableName + " WHERE id=\'" + accountGuid + "\';";
            using (var reader = ExecuteCommand(command).ExecuteReader())
            {
                if (!reader.Read()) return null;

                var id = Guid.Parse(Convert.ToString(reader["id"]));
                return new AccountModel(id);
            }
        }

        public List<AccountModel> FindAll()
        {
            var command = "SELECT * FROM " + _tableName + ";";
            using (var reader = ExecuteCommand(command).ExecuteReader())
            {
                var accounts = new List<AccountModel>();
                while (reader.Read())
                {
                    var id = Guid.Parse(Convert.ToString(reader["id"]));
                    accounts.Add(new AccountModel(id));
                }
                return accounts;
            }
        }
    }
}
