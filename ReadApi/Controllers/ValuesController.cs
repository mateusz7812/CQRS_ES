using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphQL;
using Newtonsoft.Json.Linq;

namespace ReadApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQlQuery query)
        {
            var schema = new MySchema();
            var inputs = new GraphQL.Inputs(query.Variables.AsDictionary());//.ToInputs();

            var result = await new DocumentExecuter().ExecuteAsync(
                schema.GraphQlSchema, null, query.Query, query.OperationName, inputs);

            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }

        /*   // GET api/values
           [HttpGet]
           public ActionResult<IEnumerable<string>> Get()
           {
               return new string[] { "value1", "value2" };
           }

           // GET api/values/5
           [HttpGet("{id}")]
           public ActionResult<string> Get(int id)
           {
               return "value";
           }

           // POST api/values
           [HttpPost]
           public void Post([FromBody] string value)
           {
           }

           // PUT api/values/5
           [HttpPut("{id}")]
           public void Put(int id, [FromBody] string value)
           {
           }

           // DELETE api/values/5
           [HttpDelete("{id}")]
           public void Delete(int id)
           {
           }*/
    }
}
