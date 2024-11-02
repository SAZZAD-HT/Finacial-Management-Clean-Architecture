using Core.DTos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Financial_Management_API2.Controllers
{
    public class FinanceController : ApiController
    {
       private readonly IMediator _mediator;
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [Route("AddUser")]
        public async Task<IHttpActionResult> Post([FromBody] UserDto user)
        {
            var command = _mediator.Send(user);
            return Ok(command);
        }
        
        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
