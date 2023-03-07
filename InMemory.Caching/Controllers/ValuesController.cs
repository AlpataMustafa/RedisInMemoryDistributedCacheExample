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
        public void Set(string name)
        {
            memoryCache.Set("name",name);
        }
        [HttpGet]
        public string Get()
        {
            if (memoryCache.TryGetValue<string>("name",out string name)==true)
            {
                return name.ToLower();
            }
        }
    }
}
