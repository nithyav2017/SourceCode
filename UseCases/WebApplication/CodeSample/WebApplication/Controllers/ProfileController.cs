using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using Shared.Entities;
using Shared.Entities.DTO;
using WebApplication.Interfaces;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
       private IPersonService _userService;
       private IConnectionMultiplexer _redis;
       private readonly IMemoryCache _cache;
        private readonly ISubscriber _subscriber;

        public ProfileController(IPersonService userService ,IConnectionMultiplexer redis, IMemoryCache cache)
        {
            _redis = redis;
            _userService = userService;
            _cache = cache;
            _subscriber = redis.GetSubscriber();
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 600, VaryByQueryKeys = new[] {"id"}, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Index(int id)
        {
            var cacheKey = $"Profile_{id}";
            if (!_cache.TryGetValue(cacheKey, out var profile))
            {
                Console.WriteLine("Fetching profile {id} from DB at {DateTime.Now}");
                profile = await _userService.GetProfileDTO(id);

                if(profile !=null)
                {
                    _cache.Set(cacheKey, profile, TimeSpan.FromMinutes(10));
                }
            }

            return Ok(profile);
        }

        [HttpPost("UpdateProfile/{userID}")]
        public  async Task<IActionResult> UpdateProfile(PersonProfileDto model, int userID)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState); 

            await _userService.SaveProfile(model);                     
            
             await _subscriber.PublishAsync("Profile-Invalidate",userID.ToString()); 
            
             _cache.Remove($"Profile_{userID}");
            return Ok("Profile updated and cache invalidated"); 
        }
    }
}
