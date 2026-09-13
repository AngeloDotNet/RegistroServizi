namespace RegistroServizi.Application.Services;

public class PrezzoServizioService(IRegistroServiziDbContext dbContext, IMemoryCacheService memoryCache) : IPrezzoServizioService
{
    private readonly string cacheKey = MemoryCacheHelper.CacheKeyPrezzoServizio;

    public async Task<IReadOnlyList<PrezzoServizioDto>> GetAllPrezziServiziAsync(CancellationToken cancellationToken = default)
    {
        var cacheData = await memoryCache.GetAsync<IReadOnlyList<PrezzoServizioDto>>(cacheKey);

        if (cacheData is not null)
        {
            return cacheData;
        }

        var prezziServizi = await PrezzoServizioQuery()
            .OrderBy(x => x.TipologiaServizio.TipoServizio)
            .Select(prezzoServizio => PrezzoServizioHelper.MapPrezzoServizioToDto(prezzoServizio))
            .ToListAsync(cancellationToken);

        await memoryCache.SetAsync(cacheKey, prezziServizi, MemoryCacheHelper.DefaultExpiration);

        return prezziServizi;
    }

    public async Task<PrezzoServizioDto> GetByIdPrezzoServizioAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var prezzoServizio = await PrezzoServizioQuery()
            .Where(x => x.Id == id)
            .Select(prezzoServizio => PrezzoServizioHelper.MapPrezzoServizioToDto(prezzoServizio))
            .FirstOrDefaultAsync(cancellationToken) ?? throw new KeyNotFoundException($"Prezzo servizio con id {id} non trovato.");

        return prezzoServizio;
    }

    //public async Task<PrezzoServizioDto> CreatePrezzoServizioAsync(CreatePrezzoServizioDto createDto, CancellationToken cancellationToken = default)
    //{
    //    //TODO: Validazione dei dati in ingresso (createDto) se necessario.

    //    var prezzoServizio = new PrezzoServizio
    //    {
    //        Id = Guid.NewGuid(),
    //        TipologiaServizio = createDto.TipologiaServizio,
    //        CostoFisso = createDto.CostoFisso,
    //        CostoKm = createDto.CostoKm,
    //        SecondoTrasportato = createDto.SecondoTrasportato,
    //        FermoMacchina = createDto.FermoMacchina,
    //        Accompagnatore = createDto.Accompagnatore,
    //        ScontoSocio = createDto.ScontoSocio
    //    };

    //    dbContext.PrezziServizi.Add(prezzoServizio);
    //    await dbContext.SaveChangesAsync(cancellationToken);

    //    return PrezzoServizioHelper.MapPrezzoServizioToDto(prezzoServizio);
    //}

    public async Task<PrezzoServizioDto> UpdatePrezzoServizioAsync(UpdatePrezzoServizioDto updateDto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(updateDto);

        cancellationToken.ThrowIfCancellationRequested();

        if (updateDto.Id == Guid.Empty)
        {
            throw new ArgumentException("Il campo Id non può essere vuoto.", nameof(updateDto.Id));
        }

        //TODO: Aggiungere eventuali altre validazioni dei campi in ingresso (updateDto) se necessario.

        var prezzoServizio = await dbContext.PrezziServizi.FirstOrDefaultAsync(x => x.Id == updateDto.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Prezzo servizio con id {updateDto.Id} non trovato.");

        prezzoServizio.TipologiaServizioId = updateDto.TipologiaServizioId;
        prezzoServizio.CostoFisso = updateDto.CostoFisso;
        prezzoServizio.CostoKm = updateDto.CostoKm;
        prezzoServizio.SecondoTrasportato = updateDto.SecondoTrasportato;
        prezzoServizio.FermoMacchina = updateDto.FermoMacchina;
        prezzoServizio.Accompagnatore = updateDto.Accompagnatore;
        prezzoServizio.ScontoSocio = updateDto.ScontoSocio;

        dbContext.PrezziServizi.Update(prezzoServizio);
        await dbContext.SaveChangesAsync(cancellationToken);

        await memoryCache.RemoveAsync(cacheKey);

        return PrezzoServizioHelper.MapPrezzoServizioToDto(prezzoServizio);
    }

    private IQueryable<PrezzoServizio> PrezzoServizioQuery()
        => dbContext.PrezziServizi
            .AsNoTracking()
            .Include(x => x.TipologiaServizio);
}