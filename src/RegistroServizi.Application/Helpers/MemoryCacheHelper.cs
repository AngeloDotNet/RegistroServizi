namespace RegistroServizi.Application.Helpers;

public static class MemoryCacheHelper
{
    public static TimeSpan DefaultExpiration => TimeSpan.FromHours(1);
    public static string CacheKeyApplicazione => "applicazione";
    public static string CacheKeyPrezzoServizio => "prezzoServizio";
}