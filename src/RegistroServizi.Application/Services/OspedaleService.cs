namespace RegistroServizi.Application.Services;

/// <summary>
/// Provides asynchronous create, read, update, and delete operations for <see cref="Ospedale"/> entities.
/// </summary>
/// <remarks>
/// Uses Entity Framework Core through the supplied <see cref="IRegistroServiziDbContext"/> instance.
/// The service validates required input, maps between entities and DTOs, honors cancellation tokens,
/// and detaches tracked entities after successful persistence.
/// </remarks>
/// <param name="dbContext">The database context used to query and persist hospital data.</param>
public class OspedaleService(IRegistroServiziDbContext dbContext) : IOspedaleService
{
    /// <summary>
    /// Retrieves all hospitals as <see cref="OspedaleDto"/> instances ordered by identifier.
    /// </summary>
    /// <param name="cancellationToken">The token to observe while executing the query.</param>
    /// <returns>A read-only list of hospitals ordered by <c>Id</c>.</returns>
    /// <exception cref="OperationCanceledException">Thrown when cancellation is requested.</exception>
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
    /// Retrieves a hospital by identifier.
    /// </summary>
    /// <param name="id">The identifier of the hospital to retrieve.</param>
    /// <param name="cancellationToken">The token to observe while executing the operation.</param>
    /// <returns>The matching hospital as an <see cref="OspedaleDto"/>.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when no hospital with the specified identifier exists.</exception>
    /// <exception cref="OperationCanceledException">Thrown when cancellation is requested.</exception>
    public async Task<OspedaleDto> GetOspedaleByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var items = await OspedaleQuery().ToListAsync(cancellationToken);
        var result = items.Select(ospedale => OspedaleMapper.MapOspedaleToDto(ospedale))
            .FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException($"Ospedale con id {id} non trovato.");

        return result;
    }

    /// <summary>
    /// Creates a new hospital and persists it to the database.
    /// </summary>
    /// <param name="createDto">The data used to create the hospital.</param>
    /// <param name="cancellationToken">The token to observe while executing the operation.</param>
    /// <returns>The created hospital as an <see cref="OspedaleDto"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="createDto"/> or its address is null.</exception>
    /// <exception cref="ArgumentException">Thrown when one or more required fields are invalid.</exception>
    /// <exception cref="InvalidOperationException">Thrown when saving changes fails.</exception>
    /// <exception cref="OperationCanceledException">Thrown when cancellation is requested.</exception>
    public async Task<OspedaleDto> CreateOspedaleAsync(CreateOspedaleDto createDto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(createDto);
        ArgumentNullException.ThrowIfNull(createDto.Ubicazione);

        cancellationToken.ThrowIfCancellationRequested();

        ObjectValidation.ValidateNotNullOrWhiteSpace(createDto.NomeOspedale, "Il nome dell'ospedale non può essere vuoto.", nameof(createDto.NomeOspedale));
        ObjectValidation.ValidateNotNullOrWhiteSpace(createDto.Ubicazione.Indirizzo, "L'indirizzo dell'ubicazione non può essere vuoto.", nameof(createDto.Ubicazione.Indirizzo));
        ObjectValidation.ValidateNotNullOrWhiteSpace(createDto.Ubicazione.Comune, "Il comune dell'ubicazione non può essere vuoto.", nameof(createDto.Ubicazione.Comune));
        ObjectValidation.ValidateNotNullOrWhiteSpace(createDto.Ubicazione.Provincia, "La provincia dell'ubicazione non può essere vuota.", nameof(createDto.Ubicazione.Provincia));

        ObjectValidation.ValidateIntegerGreaterOrEqualThanZero(createDto.Ubicazione.Cap, "Il CAP dell'ubicazione non può essere negativo.", nameof(createDto.Ubicazione.Cap));

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
    /// Updates an existing hospital and returns the persisted result.
    /// </summary>
    /// <param name="updateDto">The update data, including identifier and address.</param>
    /// <param name="cancellationToken">The token to observe while executing the operation.</param>
    /// <returns>The updated hospital as an <see cref="OspedaleDto"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="updateDto"/> or its address is null.</exception>
    /// <exception cref="ArgumentException">Thrown when one or more required fields are invalid.</exception>
    /// <exception cref="KeyNotFoundException">
    /// Thrown when the hospital does not exist or a concurrency conflict indicates it was removed.
    /// </exception>
    /// <exception cref="InvalidOperationException">Thrown when saving changes fails.</exception>
    /// <exception cref="OperationCanceledException">Thrown when cancellation is requested.</exception>
    public async Task<OspedaleDto> UpdateOspedaleAsync(UpdateOspedaleDto updateDto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(updateDto);
        ArgumentNullException.ThrowIfNull(updateDto.Ubicazione);

        cancellationToken.ThrowIfCancellationRequested();

        ObjectValidation.ValidateGuidNotEmpty(updateDto.Id, "Il campo Id non può essere vuoto.", nameof(updateDto.Id));
        ObjectValidation.ValidateNotNullOrWhiteSpace(updateDto.NomeOspedale, "Il nome dell'ospedale non può essere vuoto.", nameof(updateDto.NomeOspedale));
        ObjectValidation.ValidateNotNullOrWhiteSpace(updateDto.Ubicazione.Indirizzo, "L'indirizzo dell'ubicazione non può essere vuoto.", nameof(updateDto.Ubicazione.Indirizzo));
        ObjectValidation.ValidateNotNullOrWhiteSpace(updateDto.Ubicazione.Comune, "Il comune dell'ubicazione non può essere vuoto.", nameof(updateDto.Ubicazione.Comune));
        ObjectValidation.ValidateNotNullOrWhiteSpace(updateDto.Ubicazione.Provincia, "La provincia dell'ubicazione non può essere vuota.", nameof(updateDto.Ubicazione.Provincia));

        ObjectValidation.ValidateIntegerGreaterOrEqualThanZero(updateDto.Ubicazione.Cap, "Il CAP dell'ubicazione non può essere negativo.", nameof(updateDto.Ubicazione.Cap));

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
    /// Deletes a hospital by identifier.
    /// </summary>
    /// <param name="id">The identifier of the hospital to delete.</param>
    /// <param name="cancellationToken">The token to observe while executing the operation.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="id"/> is empty.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when no hospital with the specified identifier exists.</exception>
    /// <exception cref="InvalidOperationException">Thrown when saving changes fails.</exception>
    /// <exception cref="OperationCanceledException">Thrown when cancellation is requested.</exception>
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

    /// <summary>
    /// Returns the hospitals query configured for read-only access.
    /// </summary>
    /// <remarks>The query uses <see cref="EntityFrameworkQueryableExtensions.AsNoTracking{TEntity}(IQueryable{TEntity})"/>.</remarks>
    /// <returns>An <see cref="IQueryable{T}"/> over <see cref="Ospedale"/> entities.</returns>
    private IQueryable<Ospedale> OspedaleQuery() => dbContext.Ospedali.AsNoTracking();
}