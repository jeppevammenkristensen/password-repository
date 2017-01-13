using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models.Commands;
using Api.Models.Commands.Mongo;
using Api.Models.Encryption;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class PasswordsController : Controller
    {
        private readonly IPasswordCommandExecutor _commandExecutor;

        public PasswordsController(IPasswordCommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
        }

        // GET api/values
        [HttpGet]
        public async Task<GetPasswordResult> Get()
        {
            return await _commandExecutor.GetPasswordsAsync(new GetPasswordCommand());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<AddPasswordResult> Post([FromBody]AddPasswordCommand value)
        {
            return await _commandExecutor.AddPasswordAsync(value);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
