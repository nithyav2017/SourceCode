using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using Shared.Entities;
using Shared.Entities.DTO;
using WebApplication.Interfaces;
using Microsoft.AspNetCore.Identity;
using WebApplications.Interfaces;
using WebApplications.Utility;

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
    private IRegistrationService _registrationService;

        public ProfileController(IPersonService userService ,IConnectionMultiplexer redis, IMemoryCache cache, IRegistrationService registrationService)
        {
            _redis = redis;
            _userService = userService;
            _cache = cache;
            _subscriber = redis.GetSubscriber();
            _registrationService = registrationService;
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
        [HttpPost("RegisterUser")]
      
        public async Task<IActionResult>  UserRegistration(RegistrationDTO model)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            
            model.PasswordSalt = GenerateSalt.CreateSalt();

            var user = new IdentityUser { UserName = model.EmailAddress, Email = model.EmailAddress };
            
            var hasher = new PasswordHasher<IdentityUser>();

            user.PasswordHash = hasher.HashPassword(user, model.PasswordHash  );
          
            await _registrationService.CreateAsync(user,model);                     
            
            return Ok("User registered successfully");
        }

        
    }

}
