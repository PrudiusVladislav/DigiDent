using Microsoft.Extensions.Caching.Memory;

namespace DigiDent.EFCorePersistence.ClinicCore.Shared;

public class CacheConstants
{
    public static readonly TimeSpan CacheExpiration = TimeSpan.FromHours(1);
    public static readonly MemoryCacheEntryOptions DefaultCacheOptions = new()
    {
        AbsoluteExpirationRelativeToNow = CacheExpiration
    };
}