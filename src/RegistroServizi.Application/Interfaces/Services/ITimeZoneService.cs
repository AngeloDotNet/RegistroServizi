namespace RegistroServizi.Application.Interfaces.Services;

public interface ITimeZoneService
{
    string? GetTimeZoneHeaderValue();
    TimeZoneInfo? GetTimeZone();
}