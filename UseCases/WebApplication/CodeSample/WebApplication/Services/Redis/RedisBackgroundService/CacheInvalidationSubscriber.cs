
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;

namespace WebApplications.Services.Redis.RedisBackgroundService
{
    
    public class CacheInvalidationSubscriber : BackgroundService
    {
        private readonly ISubscriber _subscriber;
        private readonly IMemoryCache _memoryCache;

        public CacheInvalidationSubscriber(IConnectionMultiplexer redis, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _subscriber = redis?.GetSubscriber() ?? throw new ArgumentNullException(nameof(redis));

        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriber.Subscribe("Profile-Invalidate", (channel, value) =>
            {
                var userId = value.ToString();
                Console.WriteLine($"Invalidating cache for {userId}");
                _memoryCache.Remove($"Profile_{userId}");
            });
            return Task.CompletedTask;
        }
    }
}
