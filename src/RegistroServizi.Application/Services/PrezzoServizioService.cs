namespace RegistroServizi.Application.Services;

/// <summary>
/// Provides asynchronous operations to retrieve and update PrezzoServizio entities and map them to data transfer
/// objects (DTOs).
/// </summary>
/// <remarks>All public methods are asynchronous and accept a CancellationToken. Inputs are validated (including
/// non-negative numeric checks); failures may throw ArgumentNullException, ArgumentException, KeyNotFoundException, or
/// InvalidOperationException. Queries use AsNoTracking and include related TipoServizio; updated entities are detached
/// after saving. Concurrency and database update errors are surfaced with correlation identifiers.</remarks>
/// <param name="dbContext">The database context used to query and update PrezziServizi and related TipoServizio details.</param>
public class PrezzoServizioService(IRegistroServiziDbContext dbContext) : IPrezzoServizioService
{
    /// <summary>
    /// Asynchronously retrieves all PrezzoServizioDto instances ordered by service type.
    /// </summary>
    /// <remarks>Throws OperationCanceledException if cancellation is requested. The query is executed and
    /// results are projected to DTOs before being returned.</remarks>
    /// <param name="cancellationToken">Cancellation token to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a read-only list of
    /// PrezzoServizioDto ordered by service type.</returns>
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
    /// Gets the PrezzoServizioDto with the specified identifier asynchronously.
    /// </summary>
    /// <remarks>If cancellation is requested, an OperationCanceledException is thrown. The query projects the
    /// entity to a DTO.</remarks>
    /// <param name="id">The identifier of the PrezzoServizio to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the PrezzoServizioDto with the
    /// specified identifier.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when a PrezzoServizio with the specified id is not found.</exception>
    public async Task<PrezzoServizioDto> GetByIdPrezzoServizioAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await PrezzoServizioQuery()
            .Where(x => x.Id == id)
            .Select(prezzoServizio => PrezzoServizioMapper.MapPrezzoServizioToDto(prezzoServizio))
            .FirstOrDefaultAsync(cancellationToken) ?? throw new KeyNotFoundException($"Prezzo servizio con id {id} non trovato.");

        return result;
    }

    /// <summary>
    /// Updates an existing service price and returns the updated PrezzoServizioDto.
    /// </summary>
    /// <remarks>The DTO is mapped to the entity, changes are saved, and the entity is detached before mapping
    /// back to a DTO for the return value.</remarks>
    /// <param name="updateDto">DTO containing updated values for the service price. Id and TipologiaServizioId must be non-empty; numeric
    /// fields (CostoFisso, CostoKm, SecondoTrasportato, FermoMacchina) must be non-negative.</param>
    /// <param name="cancellationToken">CancellationToken to observe while performing the update operation.</param>
    /// <returns>The updated PrezzoServizioDto representing the persisted entity.</returns>
    /// <exception cref="ArgumentException">Thrown when updateDto is null, when Id or TipologiaServizioId is Guid.Empty, or when any numeric field is
    /// negative.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when no PrezzoServizio with the specified Id is found; concurrency conflicts are surfaced as
    /// KeyNotFoundException with a correlation id.</exception>
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

    private IQueryable<PrezzoServizio> PrezzoServizioQuery()
        => dbContext.PrezziServizi
            .AsNoTracking()
            .IncludeTipoServizioDetails();
}