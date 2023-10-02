using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;

namespace GoldAggregator.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public CacheController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpDelete]
        [Route("flush")]
        public async Task<IActionResult> DeleteFlush([FromBody] string password)
        {
            if(password == "!!!")
            {
                if (_memoryCache is MemoryCache memoryCache)
                {
                    var percentage = 1.0; // 100%
                    memoryCache.Compact(percentage);
                }

                return Ok("Successeded");
            }

            return Ok();
        }
    }
}
