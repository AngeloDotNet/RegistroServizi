namespace RegistroServizi.Application.Services;

/// <summary>
/// Provides asynchronous operations to retrieve and update <see cref="PrezzoServizio"/> entities and map them to
/// data transfer objects (DTOs).
/// </summary>
/// <remarks>
/// All public methods are asynchronous and accept a <see cref="CancellationToken"/>. Inputs are validated, including
/// non-negative numeric checks; failures may throw <see cref="ArgumentNullException"/>, <see cref="ArgumentException"/>,
/// <see cref="KeyNotFoundException"/>, or <see cref="InvalidOperationException"/>. Queries use
/// <c>AsNoTracking</c> and include related <c>TipoServizio</c>; updated entities are detached after saving.
/// Concurrency and database update errors are surfaced with correlation identifiers.
/// </remarks>
/// <param name="dbContext">The database context used to query and update <c>PrezziServizi</c> and related <c>TipoServizio</c> details.</param>
public class PrezzoServizioService(IRegistroServiziDbContext dbContext) : IPrezzoServizioService
{
    /// <summary>
    /// Asynchronously retrieves all <see cref="PrezzoServizioDto"/> instances ordered by service type.
    /// </summary>
    /// <remarks>
    /// Throws <see cref="OperationCanceledException"/> if cancellation is requested. The query is executed and
    /// results are projected to DTOs before being returned.
    /// </remarks>
    /// <param name="cancellationToken">Cancellation token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a read-only list of
    /// <see cref="PrezzoServizioDto"/> ordered by service type.</returns>
    public async Task<IReadOnlyList<PrezzoServizioDto>> GetAllPrezziServiziAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await PrezzoServizioQuery()
            .OrderBy(x => x.TipologiaServizio.TipoServizio)
            .Select(prezzoServizio => PrezzoServizioMapper.MapPrezzoServizioToDto(prezzoServizio))
            .ToListAsync(cancellationToken);

        return result;
    }

    /// <summary>
    /// Gets the <see cref="PrezzoServizioDto"/> with the specified identifier asynchronously.
    /// </summary>
    /// <remarks>
    /// If cancellation is requested, an <see cref="OperationCanceledException"/> is thrown. The query projects the
    /// entity to a DTO.
    /// </remarks>
    /// <param name="id">The identifier of the <c>PrezzoServizio</c> to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the
    /// <see cref="PrezzoServizioDto"/> with the specified identifier.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when a <c>PrezzoServizio</c> with the specified id is not found.</exception>
    public async Task<PrezzoServizioDto> GetPrezzoServizioByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await PrezzoServizioQuery()
            .Where(x => x.Id == id)
            .Select(prezzoServizio => PrezzoServizioMapper.MapPrezzoServizioToDto(prezzoServizio))
            .FirstOrDefaultAsync(cancellationToken) ?? throw new KeyNotFoundException($"Prezzo servizio con id {id} non trovato.");

        return result;
    }

    /// <summary>
    /// Updates an existing service price and returns the updated <see cref="PrezzoServizioDto"/>.
    /// </summary>
    /// <remarks>
    /// The DTO is mapped to the entity, changes are saved, and the entity is detached before mapping back to a DTO
    /// for the return value.
    /// </remarks>
    /// <param name="updateDto">DTO containing updated values for the service price. Id and TipologiaServizioId must be non-empty; numeric
    /// fields (<c>CostoFisso</c>, <c>CostoKm</c>, <c>SecondoTrasportato</c>, <c>FermoMacchina</c>) must be non-negative.</param>
    /// <param name="cancellationToken">CancellationToken to observe while performing the update operation.</param>
    /// <returns>The updated <see cref="PrezzoServizioDto"/> representing the persisted entity.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="updateDto"/> is null, when Id or TipologiaServizioId is <see cref="Guid.Empty"/>, or when any numeric field is negative.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when no <c>PrezzoServizio</c> with the specified Id is found; concurrency conflicts are surfaced as
    /// <see cref="KeyNotFoundException"/> with a correlation id.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the database update fails; the exception message includes a correlation id.</exception>
    public async Task<PrezzoServizioDto> UpdatePrezzoServizioAsync(UpdatePrezzoServizioDto updateDto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(updateDto);

        cancellationToken.ThrowIfCancellationRequested();

        if (updateDto.Id == Guid.Empty)
        {
            throw new ArgumentException("Il campo Id non può essere vuoto.", nameof(updateDto.Id));
        }

        if (updateDto.TipologiaServizioId == Guid.Empty)
        {
            throw new ArgumentException("Il campo TipologiaServizioId non può essere vuoto.", nameof(updateDto.TipologiaServizioId));
        }

        var numericChecks = new (decimal Value, string DisplayName, string ParamName)[]
        {
            (updateDto.CostoFisso, "CostoFisso", nameof(updateDto.CostoFisso)),
            (updateDto.CostoKm, "CostoKm", nameof(updateDto.CostoKm)),
            (updateDto.SecondoTrasportato, "SecondoTrasportato", nameof(updateDto.SecondoTrasportato)),
            (updateDto.FermoMacchina, "FermoMacchina", nameof(updateDto.FermoMacchina))
        };

        foreach (var (value, displayName, paramName) in numericChecks)
        {
            if (value < 0)
            {
                throw new ArgumentException($"Il campo {displayName} non può essere negativo.", paramName);
            }
        }

        var entity = await dbContext.PrezziServizi.FindAsync([updateDto.Id], cancellationToken).ConfigureAwait(false)
            ?? throw new KeyNotFoundException($"Prezzo servizio con id {updateDto.Id} non trovato.");

        entity = PrezzoServizioMapper.MapPrezzoServizioToEntityUpdate(updateDto);
        dbContext.PrezziServizi.Update(entity);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            dbContext.Entry(entity).State = EntityState.Detached;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            var correlationId = Guid.NewGuid().ToString("D");
            throw new KeyNotFoundException($"Prezzo servizio con id {updateDto.Id} non trovato. CorrelationId: {correlationId}", ex);
        }
        catch (DbUpdateException ex)
        {
            var correlationId = Guid.NewGuid().ToString("D");
            throw new InvalidOperationException($"Errore durante l'aggiornamento del prezzo servizio. CorrelationId: {correlationId}", ex);
        }

        return PrezzoServizioMapper.MapPrezzoServizioToDto(entity);
    }

    /// <summary>
    /// Builds the base query used by this service to retrieve <c>PrezziServizi</c>.
    /// </summary>
    /// <remarks>
    /// The query is configured with <c>AsNoTracking</c> and includes <c>TipoServizio</c> details for read-only
    /// operations and projection to DTOs.
    /// </remarks>
    /// <returns>A queryable sequence of <see cref="PrezzoServizio"/> entities.</returns>
    private IQueryable<PrezzoServizio> PrezzoServizioQuery()
        => dbContext.PrezziServizi
            .AsNoTracking()
            .IncludeTipoServizioDetails();
}