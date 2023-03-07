using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IMemoryCache memoryCache;

        public ValuesController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
        [HttpGet]
        public void Set()
        {
            memoryCache.Set("name", "emre");
        }
        [HttpGet]
        public string Get()
        {
            return memoryCache.Get<string>("name");
        }
    }
}
