namespace RegistroServizi.Application.Services;

public class ApplicazioneService(IRegistroServiziDbContext dbContext) : IApplicazioneService
{
    /// <summary>
    /// Recupera tutte le applicazioni presenti nel database.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IReadOnlyList<ApplicazioneDto>> GetAllApplicazioniAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await ApplicazioneQuery()
            .OrderBy(x => x.Id)
            .Select(applicazione => ApplicazioneMapper.MapApplicazioneToDto(applicazione))
            .ToListAsync(cancellationToken);

        return result;
    }

    /// <summary>
    /// Recupera un'applicazione specifica in base all'ID fornito.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public async Task<ApplicazioneDto> GetByIdApplicazioneAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await ApplicazioneQuery()
            .Select(applicazione => ApplicazioneMapper.MapApplicazioneToDto(applicazione))
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            ?? throw new KeyNotFoundException($"Applicazione con id {id} non trovato.");

        return result;
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

    /// <summary>
    /// Aggiorna un'applicazione esistente nel database.
    /// </summary>
    /// <param name="updateDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Il DTO dell'applicazione aggiornata.</returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="KeyNotFoundException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
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

        if (string.IsNullOrWhiteSpace(updateDto.Versione) || !ApplicazioneMapper.VersionRegex.IsMatch(updateDto.Versione))
        {
            throw new ArgumentException("Il campo Versione non può essere nullo o avere un formato non valido. Usa ad esempio 1.0.0.", nameof(updateDto.Versione));
        }

        if (string.IsNullOrWhiteSpace(updateDto.TimeZone))
        {
            throw new ArgumentNullException(nameof(updateDto.TimeZone), "Il campo TimeZone non può essere nullo o vuoto.");
        }

        var entity = await dbContext.Applicazioni.FindAsync([updateDto.Id], cancellationToken).ConfigureAwait(false)
            ?? throw new KeyNotFoundException($"Applicazione con id {updateDto.Id} non trovato.");

        entity = ApplicazioneMapper.MapApplicazioneToEntityUpdate(updateDto);
        dbContext.Applicazioni.Update(entity);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            dbContext.Entry(entity).State = EntityState.Detached;
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

        return ApplicazioneMapper.MapApplicazioneToDto(entity);
    }

    private IQueryable<Applicazione> ApplicazioneQuery() => dbContext.Applicazioni.AsNoTracking();
}