namespace RegistroServizi.Application.Services;

/// <summary>
/// Provides read and write operations for colonnina data.
/// </summary>
/// <remarks>Read operations use AsNoTracking. Public methods are asynchronous and accept an optional CancellationToken.</remarks>
/// <param name="dbContext">The database context used to query and persist colonnina entities.</param>
public class ColonninaService(IRegistroServiziDbContext dbContext) : IColonninaService
{
    /// <summary>
    /// Gets all colonnine as ColonninaDto instances ordered by Id.
    /// </summary>
    /// <remarks>The query is projected through ColonninaMapper and materialized asynchronously.</remarks>
    /// <param name="cancellationToken">Token used to cancel the asynchronous operation.</param>
    /// <returns>A read-only list of ColonninaDto objects ordered by Id.</returns>
    public async Task<IReadOnlyList<ColonninaDto>> GetAllColonnineAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await ColonninaQuery()
            .OrderBy(x => x.Id)
            .Select(colonnina => ColonninaMapper.MapColonninaToDto(colonnina))
            .ToListAsync(cancellationToken);

        return result;
    }

    /// <summary>
    /// Gets the ColonninaDto with the specified identifier.
    /// </summary>
    /// <remarks>The current colonnina set is materialized, mapped to DTOs, and searched for the requested identifier.</remarks>
    /// <param name="id">The identifier of the colonnina to retrieve.</param>
    /// <param name="cancellationToken">Token used to cancel the asynchronous operation.</param>
    /// <returns>The matching ColonninaDto.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when no colonnina with the specified identifier exists.</exception>
    public async Task<ColonninaDto> GetColonninaByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var items = await ColonninaQuery().ToListAsync(cancellationToken);
        var result = items.Select(colonnina => ColonninaMapper.MapColonninaToDto(colonnina))
            .FirstOrDefault(x => x.Id == id) ?? throw new KeyNotFoundException($"Colonnina con id {id} non trovato.");

        return result;
    }

    /// <summary>
    /// Creates a new colonnina, saves it to the database, and returns the created DTO.
    /// </summary>
    /// <remarks>
    /// Validates required fields, maps the DTO to an entity, persists the entity, and detaches it from the context after
    /// saving. Database failures are wrapped in an InvalidOperationException that includes a correlation identifier.
    /// </remarks>
    /// <param name="createDto">The data required to create a new colonnina.</param>
    /// <param name="cancellationToken">Token used to cancel the asynchronous operation.</param>
    /// <returns>The created ColonninaDto.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="createDto"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when saving the new colonnina fails.</exception>
    public async Task<ColonninaDto> CreateColonninaAsync(CreateColonninaDto createDto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(createDto);

        cancellationToken.ThrowIfCancellationRequested();

        ObjectValidation.ValidateNotNullOrWhiteSpace(createDto.NomeColonnina, "Il nome della colonnina non può essere vuoto.", nameof(createDto.NomeColonnina));
        ObjectValidation.ValidateNotNullOrWhiteSpace(createDto.Comune, "Il comune della colonnina non può essere vuoto.", nameof(createDto.Comune));
        ObjectValidation.ValidateNotNullOrWhiteSpace(createDto.Provincia, "La provincia della colonnina non può essere vuota.", nameof(createDto.Provincia));

        var entity = ColonninaMapper.MapColonninaToEntityCreate(createDto);
        dbContext.Colonnine.Add(entity);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            dbContext.Entry(entity).State = EntityState.Detached;
        }
        catch (Exception ex)
        {
            var correlationId = Guid.NewGuid().ToString("D");
            throw new InvalidOperationException($"Errore durante la creazione della colonnina. CorrelationId: {correlationId}", ex);
        }

        return ColonninaMapper.MapColonninaToDto(entity);
    }

    /// <summary>
    /// Updates an existing colonnina and returns the updated DTO.
    /// </summary>
    /// <remarks>
    /// Validates the input, loads the existing entity, maps the updated values, and saves the changes. Concurrency and
    /// persistence failures are wrapped with a correlation identifier when appropriate.
    /// </remarks>
    /// <param name="updateDto">The DTO containing the identifier and updated values.</param>
    /// <param name="cancellationToken">Token used to cancel the asynchronous operation.</param>
    /// <returns>The updated ColonninaDto.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="updateDto"/> is null.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when the colonnina cannot be found or a concurrency conflict indicates it no longer exists.</exception>
    /// <exception cref="InvalidOperationException">Thrown when saving the updated colonnina fails.</exception>
    public async Task<ColonninaDto> UpdateColonninaAsync(UpdateColonninaDto updateDto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(updateDto);

        cancellationToken.ThrowIfCancellationRequested();

        ObjectValidation.ValidateGuidNotEmpty(updateDto.Id, "Il campo Id non può essere vuoto.", nameof(updateDto.Id));
        ObjectValidation.ValidateNotNullOrWhiteSpace(updateDto.NomeColonnina, "Il nome della colonnina non può essere vuoto.", nameof(updateDto.NomeColonnina));
        ObjectValidation.ValidateNotNullOrWhiteSpace(updateDto.Comune, "Il comune della colonnina non può essere vuoto.", nameof(updateDto.Comune));
        ObjectValidation.ValidateNotNullOrWhiteSpace(updateDto.Provincia, "La provincia della colonnina non può essere vuota.", nameof(updateDto.Provincia));

        var entity = await dbContext.Colonnine.FindAsync([updateDto.Id], cancellationToken).ConfigureAwait(false)
            ?? throw new KeyNotFoundException($"Colonnina con id {updateDto.Id} non trovato.");

        entity = ColonninaMapper.MapColonninaToEntityUpdate(updateDto);
        dbContext.Colonnine.Update(entity);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            dbContext.Entry(entity).State = EntityState.Detached;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            var correlationId = Guid.NewGuid().ToString("D");
            throw new KeyNotFoundException($"Colonnina con id {updateDto.Id} non trovato. CorrelationId: {correlationId}", ex);
        }
        catch (DbUpdateException ex)
        {
            var correlationId = Guid.NewGuid().ToString("D");
            throw new InvalidOperationException($"Errore durante l'aggiornamento della colonnina. CorrelationId: {correlationId}", ex);
        }

        return ColonninaMapper.MapColonninaToDto(entity);
    }

    /// <summary>
    /// Deletes the specified colonnina from the database.
    /// </summary>
    /// <remarks>Validates the identifier, removes the matching entity, and saves the change asynchronously.</remarks>
    /// <param name="id">The identifier of the colonnina to delete.</param>
    /// <param name="cancellationToken">Token used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when no colonnina with the specified identifier exists.</exception>
    /// <exception cref="InvalidOperationException">Thrown when saving the delete operation fails.</exception>
    public async Task DeleteColonninaAsync(Guid id, CancellationToken cancellationToken = default)
    {
        ObjectValidation.ValidateGuidNotEmpty(id, "Il campo Id non può essere vuoto.", nameof(id));

        var entity = await dbContext.Colonnine.FindAsync([id], cancellationToken).ConfigureAwait(false)
            ?? throw new KeyNotFoundException($"Colonnina con id {id} non trovato.");

        dbContext.Colonnine.Remove(entity);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (DbUpdateException ex)
        {
            var correlationId = Guid.NewGuid().ToString("D");
            throw new InvalidOperationException($"Errore durante l'eliminazione della colonnina. CorrelationId: {correlationId}", ex);
        }
    }

    /// <summary>
    /// Returns a queryable set of colonnine configured for read-only access.
    /// </summary>
    private IQueryable<Colonnina> ColonninaQuery() => dbContext.Colonnine.AsNoTracking();
}