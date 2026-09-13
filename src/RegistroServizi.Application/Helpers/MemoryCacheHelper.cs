namespace RegistroServizi.Application.Helpers;

/// <summary>
/// Helper class for managing memory cache keys and expiration settings.
/// </summary>
public static class MemoryCacheHelper
{
    /// <summary>
    /// Gets the default expiration time for cache entries.
    /// </summary>
    public static TimeSpan DefaultExpiration => TimeSpan.FromHours(1);

    /// <summary>
    /// Gets the cache key for storing Applicazione entities.
    /// </summary>
    public static string CacheKeyApplicazione => "applicazione";

    /// <summary>
    /// Gets the cache key for storing PrezzoServizio entities.
    /// </summary>
    public static string CacheKeyPrezzoServizio => "prezzoServizio";
}