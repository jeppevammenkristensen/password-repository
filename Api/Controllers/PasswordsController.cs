using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Models.Commands;
using Api.Models.Commands.Mongo;
using Api.Models.Encryption;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class PasswordsController : Controller
    {
        private readonly IPasswordCommandExecutor _commandExecutor;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PasswordsController(IPasswordCommandExecutor commandExecutor, IHostingEnvironment hostingEnvironment)
        {
            _commandExecutor = commandExecutor;
            _hostingEnvironment = hostingEnvironment;
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

        [HttpPost("Upload")]
        public async Task<List<Guid>> UploadFiles(IList<IFormFile> uploadedFiles)
        {
            var result = new List<Guid>();

            foreach (var formFile in uploadedFiles)
            {
                var guid = Guid.NewGuid();
                var filename = Path.Combine(this._hostingEnvironment.WebRootPath,"upload", $"{guid}.txt");
                using (var fs = System.IO.File.Create(filename))
                {
                    await formFile.CopyToAsync(fs);
                    await fs.FlushAsync();
                }

                result.Add(guid);
            }

            return result;
        }
        [HttpGet("Process/{id}")]
        public async Task<object> Process(Guid id)
        {
            var path = Path.Combine(this._hostingEnvironment.WebRootPath, "upload", $"{id}.txt");
            var items = new List<string>();

            using (var file = new StreamReader(System.IO.File.OpenRead(path), Encoding.GetEncoding("iso-8859-1")))
            {
                if (file.Peek() >= 0)
                {
                    var result = await file.ReadLineAsync();
                    items.Add(result);
                }
            }

            return items;
        }
    }
}
