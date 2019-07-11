using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace jupyter_code_generator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneratorController : ControllerBase
    {
        // GET: api/Generator
        [HttpGet]
        [Route("")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Generator/5
        [HttpGet("{id}", Name = "Get")]
        //[Route("api/Generator")]
        public string Get(int id)
        {
            return "value";
        }
        // GET: api/Generator/authCode
        [HttpGet]
        [Route("authCode")]
        [Authorize]
        public string GetAuthCode()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId").Value;
            var authCode = Startup.userTokenCache[userId];
            return userId;
        }

        // POST: api/Generator
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Generator/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/GeneratorWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
