namespace RegistroServizi.Application.Mapping;

public static class OspedaleMapper
{
    public static OspedaleDto MapOspedaleToDto(Ospedale ospedale) => new OspedaleDto
    {
        Id = ospedale.Id,
        NomeOspedale = ospedale.NomeOspedale,
        Indirizzo = new IndirizzoDto
        {
            Strada = ospedale.Indirizzo.Strada,
            Citta = ospedale.Indirizzo.Citta,
            Provincia = ospedale.Indirizzo.Provincia,
            Cap = ospedale.Indirizzo.Cap ?? 0
        }
    };

    public static Ospedale MapOspedaleToEntityCreate(CreateOspedaleDto dtoCreate) => new Ospedale
    {
        Id = Guid.NewGuid(),
        NomeOspedale = dtoCreate.NomeOspedale,
        Indirizzo = new Indirizzo(dtoCreate.Indirizzo.Strada, dtoCreate.Indirizzo.Citta, dtoCreate.Indirizzo.Provincia, dtoCreate.Indirizzo.Cap ?? 0),
        Coordinate = new Coordinate(0, 0)
    };

    public static Ospedale MapOspedaleToEntityUpdate(UpdateOspedaleDto dtoUpdate) => new Ospedale
    {
        Id = dtoUpdate.Id,
        NomeOspedale = dtoUpdate.NomeOspedale,
        Indirizzo = new Indirizzo(dtoUpdate.Indirizzo.Strada, dtoUpdate.Indirizzo.Citta, dtoUpdate.Indirizzo.Provincia, dtoUpdate.Indirizzo.Cap ?? 0),
        Coordinate = new Coordinate(0, 0)
    };
}