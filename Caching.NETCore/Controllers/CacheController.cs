using Caching.NETCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Caching.NETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public CacheController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet("{Key}")]
        public IActionResult GetCache(string key)
        {
            string value = string.Empty;
            _memoryCache.TryGetValue(key, out value);

            return Ok(value);
        }
        [HttpPost]
        public IActionResult SetCache( CacheRequest data)
        {
            var memoryCache = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(2),
                Size = 1024,
            };

            _memoryCache.Set(data.key,data.value , memoryCache);
            return Ok();
        }

    }
}
