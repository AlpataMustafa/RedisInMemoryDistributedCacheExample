using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Distributed.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IDistributedCache distributedCache;

        public ValuesController(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        [HttpGet("set")]
        public async Task<IActionResult> Set(string name, string surname)
        {
            await distributedCache.SetStringAsync("name", name, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30), 
                SlidingExpiration = TimeSpan.FromSeconds(10)
            });
            await distributedCache.SetAsync("surname", Encoding.UTF8.GetBytes(surname));
            return Ok();
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var name = await distributedCache.GetStringAsync("name");
            var nameBinary = await distributedCache.GetAsync("surname");
            var surname = Encoding.UTF8.GetString(nameBinary);
            return Ok(new
            {
                name,
                nameBinary,
                surname
            });
        }
    }
}
