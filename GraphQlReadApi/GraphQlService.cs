using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using EventHandlers.Models;
using EventHandlers.Repositories;
using GraphQL;
using GraphQL.Types;

namespace GraphQlReadApi
{
    public class GraphQlService
    {
        private static AccountSqlLiteRepository _repository;
        private static ISchema _schema;

        // ReSharper disable once ClassNeverInstantiated.Local
        private class Query
        {
            [GraphQLMetadata("accounts")]
            public IEnumerable<Account> GetAccounts()
            {
                return _repository.FindAll();
            }

            [GraphQLMetadata("account")]
            public Account GetAccount(Guid accountGuid)
            {
                return _repository.FindById(accountGuid);
            }

        }

        public GraphQlService(AccountSqlLiteRepository repository)
        {
            _repository = repository;
            Configure();
        }

        private static void Configure()
        {
            _schema = Schema.For(@"
                type Account{
                    id: String
                }

                type Query{
                    accounts: [Account]
                    account(id: String): Account
            ", _ => _.Types.Include<Query>());
            
        }

        public string Handle(string query)
        {
            return _schema.ExecuteAsync(query);
        }
    }
}
