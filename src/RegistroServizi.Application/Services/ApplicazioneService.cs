namespace RegistroServizi.Application.Services;

public class ApplicazioneService(IRegistroServiziDbContext dbContext, IMemoryCacheService memoryCache) : IApplicazioneService
{
    private readonly string cacheKey = MemoryCacheHelper.CacheKeyApplicazione;

    public async Task<IReadOnlyList<ApplicazioneDto>> GetAllApplicazioniAsync(CancellationToken cancellationToken = default)
    {
        var cacheData = await memoryCache.GetAsync<IReadOnlyList<ApplicazioneDto>>(cacheKey);

        if (cacheData is not null)
        {
            return cacheData;
        }

        var result = await ApplicazioneQuery()
            .OrderBy(x => x.Id)
            .Select(applicazione => ApplicazioneHelper.MapApplicazioneToDto(applicazione))
            .ToListAsync(cancellationToken);

        await memoryCache.SetAsync(cacheKey, result, MemoryCacheHelper.DefaultExpiration);

        return result;
    }

    public async Task<ApplicazioneDto> GetByIdApplicazioneAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var applicazione = await ApplicazioneQuery()
            .Select(applicazione => ApplicazioneHelper.MapApplicazioneToDto(applicazione))
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            ?? throw new KeyNotFoundException($"Applicazione con id {id} non trovato.");

        return applicazione;
    }

    //public async Task<ApplicazioneDto> CreateApplicazioneAsync(CreateApplicazioneDto createDto, CancellationToken cancellationToken = default)
    //{
    //    //TODO: Validazione dei dati in ingresso (createDto) se necessario.

    //    var applicazione = new Applicazione
    //    {
    //        Id = Guid.NewGuid(),
    //        NomeApplicazione = createDto.NomeApplicazione,
    //        Versione = createDto.Versione
    //    };

    //    dbContext.Applicazioni.Add(applicazione);
    //    await dbContext.SaveChangesAsync(cancellationToken);

    //    return ApplicazioneHelper.MapApplicazioneToDto(applicazione);
    //}

    public async Task<ApplicazioneDto> UpdateApplicazioneAsync(UpdateApplicazioneDto updateDto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(updateDto);

        cancellationToken.ThrowIfCancellationRequested();

        if (updateDto.Id == Guid.Empty)
        {
            throw new ArgumentException("Il campo Id non può essere vuoto.", nameof(updateDto.Id));
        }

        if (string.IsNullOrWhiteSpace(updateDto.NomeApplicazione))
        {
            throw new ArgumentNullException(nameof(updateDto.NomeApplicazione), "Il campo NomeApplicazione non può essere nullo o vuoto.");
        }

        if (string.IsNullOrWhiteSpace(updateDto.Versione) || !ApplicazioneHelper.VersionRegex.IsMatch(updateDto.Versione))
        {
            throw new ArgumentException("Il campo Versione non può essere nullo o avere un formato non valido. Usa ad esempio 1.0.0.", nameof(updateDto.Versione));
        }

        if (string.IsNullOrWhiteSpace(updateDto.TimeZone))
        {
            throw new ArgumentNullException(nameof(updateDto.TimeZone), "Il campo TimeZone non può essere nullo o vuoto.");
        }

        //var applicazione = await dbContext.Applicazioni.FirstOrDefaultAsync(x => x.Id == updateDto.Id, cancellationToken).ConfigureAwait(false)
        var applicazione = await dbContext.Applicazioni.FirstOrDefaultAsync(x => x.Id == updateDto.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Applicazione con id {updateDto.Id} non trovato.");

        applicazione.NomeApplicazione = updateDto.NomeApplicazione;
        applicazione.Versione = updateDto.Versione;
        applicazione.TimeZone = updateDto.TimeZone;

        try
        {
            //await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            var correlationId = Guid.NewGuid().ToString("D");
            throw new KeyNotFoundException($"Applicazione con id {updateDto.Id} non trovato. CorrelationId: {correlationId}", ex);
        }
        catch (DbUpdateException ex)
        {
            var correlationId = Guid.NewGuid().ToString("D");
            throw new InvalidOperationException($"Errore durante l'aggiornamento dell'applicazione. CorrelationId: {correlationId}", ex);
        }

        await memoryCache.RemoveAsync(cacheKey).ConfigureAwait(false);

        return ApplicazioneHelper.MapApplicazioneToDto(applicazione);
    }

    private IQueryable<Applicazione> ApplicazioneQuery() => dbContext.Applicazioni.AsNoTracking();
}