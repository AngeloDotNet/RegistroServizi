namespace RegistroServizi.Application.Interfaces;

public interface ITimeZoneService
{
    string? GetTimeZoneHeaderValue();
    TimeZoneInfo? GetTimeZone();
}