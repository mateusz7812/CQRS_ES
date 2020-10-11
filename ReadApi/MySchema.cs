using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHandlers.Models;
using GraphQL;
using GraphQL.Types;

namespace ReadApi
{
    public class MySchema
    {
        private ISchema Schema { get; set; }
        public ISchema GraphQlSchema => this.Schema;

        public MySchema()
        {
            this.Schema = new Schema();
            Schema.RegisterType<Account>();

        }

    }
}

