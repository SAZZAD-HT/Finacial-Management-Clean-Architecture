using Application.Queries;
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
{[RoutePrefix("api/finance")]
    public class FinanceController : ApiController
    {
        private readonly IMediator _mediator;
        public FinanceController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("GetUserAndAccountInfo")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var data = await _mediator.Send(new AccountAndUserQuires { AccountId = id });
            return Ok(data);
        }

     

        // POST api/values
        [HttpPost]
        [Route("AddUser")]
        public async Task<IHttpActionResult> Post([FromBody] UserDto user)
        {
            var command = await _mediator.Send(user);
            return Ok(command);
        }

    }
}
