namespace RegistroServizi.Application.Services;

/// <summary>
/// Provides create, read, update, and delete operations for Ospedale entities using the supplied database context.
/// </summary>
/// <remarks>Uses Entity Framework Core via the provided context; methods honor CancellationToken, perform input
/// validation, map entities to DTOs, and detach tracked entities after persistence. Throws ArgumentException,
/// ArgumentNullException, KeyNotFoundException, and InvalidOperationException for validation and persistence
/// errors.</remarks>
/// <param name="dbContext">The database context used to query and persist Ospedale entities.</param>
public class OspedaleService(IRegistroServiziDbContext dbContext) : IOspedaleService
{
    /// <summary>
    /// Gets all hospitals as OspedaleDto instances ordered by Id.
    /// </summary>
    /// <remarks>Executes the query asynchronously against the data source and maps entities to OspedaleDto.
    /// The operation may throw OperationCanceledException if cancellation is requested.</remarks>
    /// <param name="cancellationToken">Cancellation token to observe while awaiting the asynchronous operation.</param>
    /// <returns>A read-only list of OspedaleDto containing all hospitals ordered by Id.</returns>
    public async Task<IReadOnlyList<OspedaleDto>> GetAllOspedaliAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await OspedaleQuery()
            .OrderBy(x => x.Id)
            .Select(ospedale => OspedaleMapper.MapOspedaleToDto(ospedale))
            .ToListAsync(cancellationToken);

        return result;
    }

    /// <summary>
    /// Retrieves an OspedaleDto with the specified identifier.
    /// </summary>
    /// <remarks>Supports cancellation via the provided token; may throw OperationCanceledException if
    /// canceled.</remarks>
    /// <param name="id">The identifier of the Ospedale to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task whose result is the OspedaleDto with the specified identifier.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when no Ospedale with the specified identifier exists.</exception>
    public async Task<OspedaleDto> GetOspedaleByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var items = await OspedaleQuery().ToListAsync(cancellationToken);
        var result = items.Select(ospedale => OspedaleMapper.MapOspedaleToDto(ospedale))
            .FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException($"Ospedale con id {id} non trovato.");

        return result;
    }

    /// <summary>
    /// Creates a new Ospedale from the provided CreateOspedaleDto and persists it to the database asynchronously.
    /// </summary>
    /// <remarks>Validates required fields (including address), maps the DTO to an entity, saves changes,
    /// detaches the tracked entity, and maps the entity back to a DTO.</remarks>
    /// <param name="createDto">CreateOspedaleDto containing hospital and address data to persist.</param>
    /// <param name="cancellationToken">CancellationToken to observe while awaiting the asynchronous operation.</param>
    /// <returns>The created OspedaleDto representing the persisted entity.</returns>
    /// <exception cref="InvalidOperationException">Thrown if saving changes to the database fails; the exception message includes a correlation identifier.</exception>
    public async Task<OspedaleDto> CreateOspedaleAsync(CreateOspedaleDto createDto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(createDto);
        ArgumentNullException.ThrowIfNull(createDto.Indirizzo);

        cancellationToken.ThrowIfCancellationRequested();

        ObjectValidation.ValidateNotNullOrWhiteSpace(createDto.NomeOspedale, "Il nome dell'ospedale non può essere vuoto.", nameof(createDto.NomeOspedale));
        ObjectValidation.ValidateNotNullOrWhiteSpace(createDto.Indirizzo.Strada, "La strada dell'indirizzo non può essere vuota.", nameof(createDto.Indirizzo.Strada));
        ObjectValidation.ValidateNotNullOrWhiteSpace(createDto.Indirizzo.Citta, "La città dell'indirizzo non può essere vuota.", nameof(createDto.Indirizzo.Citta));
        ObjectValidation.ValidateNotNullOrWhiteSpace(createDto.Indirizzo.Provincia, "La provincia dell'indirizzo non può essere vuota.", nameof(createDto.Indirizzo.Provincia));

        ObjectValidation.ValidateIntegerGreaterOrEqualThanZero(createDto.Indirizzo.Cap, "Il CAP dell'indirizzo non può essere negativo.", nameof(createDto.Indirizzo.Cap));

        var entity = OspedaleMapper.MapOspedaleToEntityCreate(createDto);
        dbContext.Ospedali.Add(entity);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            dbContext.Entry(entity).State = EntityState.Detached;
        }
        catch (Exception ex)
        {
            var correlationId = Guid.NewGuid().ToString("D");
            throw new InvalidOperationException($"Errore durante la creazione dell'ospedale. CorrelationId: {correlationId}", ex);
        }

        return OspedaleMapper.MapOspedaleToDto(entity);
    }

    /// <summary>
    /// Updates an existing hospital from the supplied UpdateOspedaleDto and returns the updated OspedaleDto.
    /// </summary>
    /// <remarks>Validates input and address fields, updates the entity, saves changes with concurrency
    /// handling, and detaches the tracked entity after a successful save.</remarks>
    /// <param name="updateDto">Update data for the hospital, including address and identifier; must not be null and must contain valid values.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>The updated OspedaleDto representing the persisted hospital.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when a hospital with the specified Id is not found, including cases where a concurrency conflict
    /// indicates the entity no longer exists. The exception message contains a correlation Id for diagnostics.</exception>
    /// <exception cref="InvalidOperationException">Thrown when an error occurs while saving changes to the database. The exception message contains a correlation
    /// Id for diagnostics.</exception>
    public async Task<OspedaleDto> UpdateOspedaleAsync(UpdateOspedaleDto updateDto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(updateDto);
        ArgumentNullException.ThrowIfNull(updateDto.Indirizzo);

        cancellationToken.ThrowIfCancellationRequested();

        ObjectValidation.ValidateGuidNotEmpty(updateDto.Id, "Il campo Id non può essere vuoto.", nameof(updateDto.Id));
        ObjectValidation.ValidateNotNullOrWhiteSpace(updateDto.NomeOspedale, "Il nome dell'ospedale non può essere vuoto.", nameof(updateDto.NomeOspedale));
        ObjectValidation.ValidateNotNullOrWhiteSpace(updateDto.Indirizzo.Strada, "La strada dell'indirizzo non può essere vuota.", nameof(updateDto.Indirizzo.Strada));
        ObjectValidation.ValidateNotNullOrWhiteSpace(updateDto.Indirizzo.Citta, "La città dell'indirizzo non può essere vuota.", nameof(updateDto.Indirizzo.Citta));
        ObjectValidation.ValidateNotNullOrWhiteSpace(updateDto.Indirizzo.Provincia, "La provincia dell'indirizzo non può essere vuota.", nameof(updateDto.Indirizzo.Provincia));

        ObjectValidation.ValidateIntegerGreaterOrEqualThanZero(updateDto.Indirizzo.Cap, "Il CAP dell'indirizzo non può essere negativo.", nameof(updateDto.Indirizzo.Cap));

        var entity = await dbContext.Ospedali.FindAsync([updateDto.Id], cancellationToken).ConfigureAwait(false)
            ?? throw new KeyNotFoundException($"Ospedale con id {updateDto.Id} non trovato.");

        entity = OspedaleMapper.MapOspedaleToEntityUpdate(updateDto);
        dbContext.Ospedali.Update(entity);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            dbContext.Entry(entity).State = EntityState.Detached;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            var correlationId = Guid.NewGuid().ToString("D");
            throw new KeyNotFoundException($"Ospedale con id {updateDto.Id} non trovato. CorrelationId: {correlationId}", ex);
        }
        catch (DbUpdateException ex)
        {
            var correlationId = Guid.NewGuid().ToString("D");
            throw new InvalidOperationException($"Errore durante l'aggiornamento dell'ospedale. CorrelationId: {correlationId}", ex);
        }

        return OspedaleMapper.MapOspedaleToDto(entity);
    }

    /// <summary>
    /// Deletes the hospital entity with the specified identifier from the database.
    /// </summary>
    /// <remarks>Validates the id is not Guid.Empty before lookup. A correlation id is generated and included
    /// in the error message when SaveChangesAsync fails.</remarks>
    /// <param name="id">The identifier of the hospital to delete. Must not be Guid.Empty.</param>
    /// <param name="cancellationToken">A token to observe for cancellation while performing the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if no hospital with the specified identifier is found.</exception>
    /// <exception cref="InvalidOperationException">Thrown if saving changes to the database fails; the inner exception contains the original DbUpdateException and
    /// the message includes a correlation identifier.</exception>
    public async Task DeleteOspedaleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        ObjectValidation.ValidateGuidNotEmpty(id, "Il campo Id non può essere vuoto.", nameof(id));

        var entity = await dbContext.Ospedali.FindAsync([id], cancellationToken).ConfigureAwait(false)
            ?? throw new KeyNotFoundException($"Ospedale con id {id} non trovato.");

        dbContext.Ospedali.Remove(entity);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (DbUpdateException ex)
        {
            var correlationId = Guid.NewGuid().ToString("D");
            throw new InvalidOperationException($"Errore durante l'eliminazione dell'ospedale. CorrelationId: {correlationId}", ex);
        }
    }

    private IQueryable<Ospedale> OspedaleQuery() => dbContext.Ospedali.AsNoTracking();
}