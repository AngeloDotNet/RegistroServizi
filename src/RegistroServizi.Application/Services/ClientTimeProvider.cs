using RegistroServizi.Application.Interfaces;

namespace RegistroServizi.Application.Services;

public class ClientTimeProvider(ITimeZoneService timeZoneService) : TimeProvider
{
    public override TimeZoneInfo LocalTimeZone => timeZoneService.GetTimeZone() ?? TimeZoneInfo.Utc;
}