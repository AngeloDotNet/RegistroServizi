namespace RegistroServizi.Application.Services;

/// <summary>
/// Provides read-only operations to retrieve Colonnina entities and map them to ColonninaDto.
/// </summary>
/// <remarks>Queries are executed with AsNoTracking. Methods are asynchronous and accept a CancellationToken.
/// GetByIdColonninaAsync throws KeyNotFoundException if a Colonnina with the specified id is not found.</remarks>
/// <param name="dbContext">The IRegistroServiziDbContext used to access the Colonnine DbSet for queries.</param>
public class ColonninaService(IRegistroServiziDbContext dbContext) : IColonninaService
{
    /// <summary>
    /// Gets all colonnine as ColonninaDto objects ordered by Id.
    /// </summary>
    /// <remarks>Throws OperationCanceledException if cancellation is requested. Projects entities to
    /// ColonninaDto using ColonninaMapper and materializes the results asynchronously via ToListAsync.</remarks>
    /// <param name="cancellationToken">Token to monitor for cancellation of the asynchronous operation.</param>
    /// <returns>A read-only list of ColonninaDto ordered by Id.</returns>
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
    /// Asynchronously retrieves the ColonninaDto with the specified identifier.
    /// </summary>
    /// <remarks>Projects the entity to a ColonninaDto and honors the provided cancellation token.</remarks>
    /// <param name="id">The identifier of the colonnina to retrieve.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that resolves to the ColonninaDto with the specified identifier.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if no colonnina with the specified identifier is found.</exception>
    public async Task<ColonninaDto> GetByIdColonninaAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await ColonninaQuery()
            .Select(colonnina => ColonninaMapper.MapColonninaToDto(colonnina))
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new KeyNotFoundException($"Colonnina con id {id} non trovato.");

        return result;
    }

    /// <summary>
    /// Creates a new Colonnina from the provided DTO, persists it to the database asynchronously, and returns the created ColonninaDto.
    /// </summary>
    /// <remarks>Validates input (throws ArgumentNullException or validation exceptions for missing or empty
    /// fields), maps the DTO to an entity, saves changes, and detaches the entity from the DbContext before mapping back to a DTO.</remarks>
    /// <param name="createDto">DTO with the data required to create a Colonnina (including NomeColonnina, Comune and Provincia); must not be
    /// null and required fields must not be empty.</param>
    /// <param name="cancellationToken">CancellationToken to observe while waiting for the operation to complete; optional.</param>
    /// <returns>The created ColonninaDto representing the persisted entity.</returns>
    /// <exception cref="InvalidOperationException">Thrown when an error occurs while saving changes to the database; the exception message contains a correlation
    /// identifier for troubleshooting.</exception>
    public async Task<ColonninaDto> CreateColonninaAsync(CreateColonninaDto createDto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(createDto);

        cancellationToken.ThrowIfCancellationRequested();

        DependencyInjection.ValidateNotNullOrWhiteSpace(createDto.NomeColonnina, "Il nome della colonnina non può essere vuoto.", nameof(createDto.NomeColonnina));
        DependencyInjection.ValidateNotNullOrWhiteSpace(createDto.Comune, "La città della colonnina non può essere vuota.", nameof(createDto.Comune));
        DependencyInjection.ValidateNotNullOrWhiteSpace(createDto.Provincia, "La provincia della colonnina non può essere vuota.", nameof(createDto.Provincia));

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
            throw new InvalidOperationException($"Errore durante la creazione dell'ospedale. CorrelationId: {correlationId}", ex);
        }

        return ColonninaMapper.MapColonninaToDto(entity);
    }

    /// <summary>
    /// Updates an existing colonnina using values from updateDto and persists the changes to the database.
    /// </summary>
    /// <param name="updateDto">Data transfer object containing the Id and the updated properties for the colonnina.</param>
    /// <param name="cancellationToken">Token to observe while waiting for the operation to complete.</param>
    /// <returns>The updated ColonninaDto representing the persisted entity.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when a colonnina with the specified Id is not found or if a concurrency conflict indicates the entity no
    /// longer exists.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the database update fails due to an underlying persistence error; the exception message includes a
    /// correlation id.</exception>
    public async Task<ColonninaDto> UpdateColonninaAsync(UpdateColonninaDto updateDto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(updateDto);

        cancellationToken.ThrowIfCancellationRequested();

        DependencyInjection.ValidateGuidNotEmpty(updateDto.Id, "Il campo Id non può essere vuoto.", nameof(updateDto.Id));
        DependencyInjection.ValidateNotNullOrWhiteSpace(updateDto.NomeColonnina, "Il nome della colonnina non può essere vuoto.", nameof(updateDto.NomeColonnina));
        DependencyInjection.ValidateNotNullOrWhiteSpace(updateDto.Comune, "La città della colonnina non può essere vuota.", nameof(updateDto.Comune));
        DependencyInjection.ValidateNotNullOrWhiteSpace(updateDto.Provincia, "La provincia della colonnina non può essere vuota.", nameof(updateDto.Provincia));

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
    /// Deletes the specified colonnina from the database asynchronously.
    /// </summary>
    /// <remarks>Validates that id is not Guid.Empty. Any DbUpdateException from SaveChangesAsync is wrapped in an InvalidOperationException with a correlation id.</remarks>
    /// <param name="id">Identifier of the colonnina to delete. Must be a non-empty GUID.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if no colonnina with the specified id is found.</exception>
    /// <exception cref="InvalidOperationException">Thrown if saving changes to the database fails; the inner exception contains database error details and the
    /// message includes a generated correlation identifier.</exception>
    public async Task DeleteColonninaAsync(Guid id, CancellationToken cancellationToken = default)
    {
        DependencyInjection.ValidateGuidNotEmpty(id, "Il campo Id non può essere vuoto.", nameof(id));

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

    private IQueryable<Colonnina> ColonninaQuery() => dbContext.Colonnine.AsNoTracking();
}