namespace RegistroServizi.Application.Services;

public class PrezzoServizioService(IRegistroServiziDbContext dbContext) : IPrezzoServizioService
{
    public async Task<IReadOnlyList<PrezzoServizioDto>> GetAllPrezziServiziAsync(CancellationToken cancellationToken = default)
    {
        var prezziServizi = await PrezzoServizioQuery()
            .OrderBy(x => x.Id)
            .Select(prezzoServizio => PrezzoServizioHelper.MapPrezzoServizioToDto(prezzoServizio))
            .ToListAsync(cancellationToken);

        return prezziServizi;
    }

    public async Task<PrezzoServizioDto> GetByIdPrezzoServizioAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var prezzoServizio = await PrezzoServizioQuery()
            .Select(prezzoServizio => PrezzoServizioHelper.MapPrezzoServizioToDto(prezzoServizio))
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            ?? throw new KeyNotFoundException($"Prezzo servizio con id {id} non trovato.");

        return prezzoServizio;
    }

    public async Task<PrezzoServizioDto> CreatePrezzoServizioAsync(CreatePrezzoServizioDto createDto, CancellationToken cancellationToken = default)
    {
        //TODO: Validazione dei dati in ingresso (createDto) se necessario.

        var prezzoServizio = new PrezzoServizio
        {
            Id = Guid.NewGuid(),
            TipologiaServizio = createDto.TipologiaServizio,
            CostoFisso = createDto.CostoFisso,
            CostoKm = createDto.CostoKm,
            SecondoTrasportato = createDto.SecondoTrasportato,
            FermoMacchina = createDto.FermoMacchina,
            Accompagnatore = createDto.Accompagnatore,
            ScontoSocio = createDto.ScontoSocio
        };

        dbContext.PrezziServizi.Add(prezzoServizio);
        await dbContext.SaveChangesAsync(cancellationToken);

        return PrezzoServizioHelper.MapPrezzoServizioToDto(prezzoServizio);
    }

    public async Task<PrezzoServizioDto> UpdatePrezzoServizioAsync(UpdatePrezzoServizioDto updateDto, CancellationToken cancellationToken = default)
    {
        //TODO: Validazione dei dati in ingresso (updateDto) se necessario.

        var prezzoServizio = await dbContext.PrezziServizi
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == updateDto.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Prezzo servizio con id {updateDto.Id} non trovato.");

        prezzoServizio.TipologiaServizio = updateDto.TipologiaServizio;
        prezzoServizio.CostoFisso = updateDto.CostoFisso;
        prezzoServizio.CostoKm = updateDto.CostoKm;
        prezzoServizio.SecondoTrasportato = updateDto.SecondoTrasportato;
        prezzoServizio.FermoMacchina = updateDto.FermoMacchina;
        prezzoServizio.Accompagnatore = updateDto.Accompagnatore;
        prezzoServizio.ScontoSocio = updateDto.ScontoSocio;

        dbContext.PrezziServizi.Update(prezzoServizio);
        await dbContext.SaveChangesAsync(cancellationToken);

        return PrezzoServizioHelper.MapPrezzoServizioToDto(prezzoServizio);
    }

    private IQueryable<PrezzoServizio> PrezzoServizioQuery() => dbContext.PrezziServizi.AsNoTracking();
}