using Microsoft.AspNetCore.Http;
using RegistroServizi.Application.Interfaces;

namespace RegistroServizi.Application.Services;

public class TimeZoneService(IHttpContextAccessor httpContextAccessor) : ITimeZoneService
{
    public static readonly string HeaderKey = "x-time-zone";

    public TimeZoneInfo? GetTimeZone()
    {
        var timeZoneId = GetTimeZoneHeaderValue();

        if (timeZoneId is null || !TimeZoneInfo.TryFindSystemTimeZoneById(timeZoneId, out var timeZoneInfo))
        {
            return null;
        }

        return timeZoneInfo;
    }

    public string? GetTimeZoneHeaderValue()
    {
        if (httpContextAccessor.HttpContext?.Request?.Headers?.TryGetValue(HeaderKey, out var timeZone) == true)
        {
            return timeZone.ToString();
        }

        return null;
    }
}