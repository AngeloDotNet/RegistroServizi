namespace RegistroServizi.Application.Services;

/// <summary>
/// Provides asynchronous operations for managing Applicazione entities and mapping them to DTOs.
/// </summary>
/// <remarks>Operations are asynchronous, perform entity-to-DTO projection, and honor cancellation tokens. Input
/// validation and concurrency or update failures surface as standard exceptions (ArgumentException,
/// ArgumentNullException, KeyNotFoundException, InvalidOperationException).</remarks>
/// <param name="dbContext">The database context used to query and persist Applicazione entities (IRegistroServiziDbContext).</param>
public class ApplicazioneService(IRegistroServiziDbContext dbContext) : IApplicazioneService
{
    /// <summary>
    /// Gets all applicazioni projected to ApplicazioneDto and ordered by Id.
    /// </summary>
    /// <remarks>Observes cancellation before executing the query; entities are projected to DTOs and
    /// materialized asynchronously.</remarks>
    /// <param name="cancellationToken">Cancellation token to cancel the asynchronous operation.</param>
    /// <returns>A task that returns a read-only list of ApplicazioneDto ordered by Id.</returns>
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
    /// Asynchronously gets the ApplicazioneDto with the specified identifier.
    /// </summary>
    /// <remarks>Queries the data source and maps the entity to ApplicazioneDto. Cancellation is
    /// honored.</remarks>
    /// <param name="id">Identifier of the Applicazione to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token to observe while awaiting the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the matching ApplicazioneDto.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when an Applicazione with the specified identifier is not found.</exception>
    public async Task<ApplicazioneDto> GetByIdApplicazioneAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await ApplicazioneQuery()
            .Select(applicazione => ApplicazioneMapper.MapApplicazioneToDto(applicazione))
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            ?? throw new KeyNotFoundException($"Applicazione con id {id} non trovato.");

        return result;
    }

    /// <summary>
    /// Updates an existing application identified by updateDto.Id and persists the changes to the database.
    /// </summary>
    /// <remarks>After a successful save the entity is detached from the DbContext. Version values must match
    /// the expected format (for example: 1.0.0).</remarks>
    /// <param name="updateDto">DTO containing the application Id and the values to update.</param>
    /// <param name="cancellationToken">CancellationToken to cancel the asynchronous operation.</param>
    /// <returns>The updated ApplicazioneDto reflecting the persisted state.</returns>
    /// <exception cref="ArgumentException">Thrown when updateDto.Id is Guid.Empty or when updateDto.Version is null, whitespace, or does not match the
    /// required version format (for example: 1.0.0).</exception>
    /// <exception cref="ArgumentNullException">Thrown when updateDto is null, or when updateDto.NomeApplicazione or updateDto.TimeZone is null or whitespace.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when an application with the specified Id is not found. In case of a concurrency conflict the exception
    /// message includes a correlation Id.</exception>
    /// <exception cref="InvalidOperationException">Thrown when a database update error occurs; the exception message includes a correlation Id.</exception>
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