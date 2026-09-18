namespace RegistroServizi.Application.Services;

/// <summary>
/// Servizio per la gestione degli ospedali.
/// </summary>
/// <param name="dbContext"></param>
public class OspedaleService(IRegistroServiziDbContext dbContext) : IOspedaleService
{
    /// <summary>
    /// Recupera tutti gli ospedali presenti nel database.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
    /// Recupera un ospedale specifico in base all'ID fornito.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public async Task<OspedaleDto> GetByIdOspedaleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await OspedaleQuery()
            .Select(ospedale => OspedaleMapper.MapOspedaleToDto(ospedale))
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken) ?? throw new KeyNotFoundException($"Ospedale con id {id} non trovato.");

        return result;
    }

    /// <summary>
    /// Crea un nuovo ospedale nel database.
    /// </summary>
    /// <param name="createDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="KeyNotFoundException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<OspedaleDto> CreateOspedaleAsync(CreateOspedaleDto createDto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(createDto);
        ArgumentNullException.ThrowIfNull(createDto.Indirizzo);

        cancellationToken.ThrowIfCancellationRequested();

        DependencyInjection.ValidateNotNullOrWhiteSpace(createDto.NomeOspedale, "Il nome dell'ospedale non può essere vuoto.", nameof(createDto.NomeOspedale));
        DependencyInjection.ValidateNotNullOrWhiteSpace(createDto.Indirizzo.Strada, "La strada dell'indirizzo non può essere vuota.", nameof(createDto.Indirizzo.Strada));
        DependencyInjection.ValidateNotNullOrWhiteSpace(createDto.Indirizzo.Citta, "La città dell'indirizzo non può essere vuota.", nameof(createDto.Indirizzo.Citta));
        DependencyInjection.ValidateNotNullOrWhiteSpace(createDto.Indirizzo.Provincia, "La provincia dell'indirizzo non può essere vuota.", nameof(createDto.Indirizzo.Provincia));

        DependencyInjection.ValidateIntegerGreaterOrEqualThanZero(createDto.Indirizzo.Cap, "Il CAP dell'indirizzo non può essere negativo.", nameof(createDto.Indirizzo.Cap));

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
    /// Aggiorna un ospedale esistente nel database.
    /// </summary>
    /// <param name="updateDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="KeyNotFoundException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<OspedaleDto> UpdateOspedaleAsync(UpdateOspedaleDto updateDto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(updateDto);
        ArgumentNullException.ThrowIfNull(updateDto.Indirizzo);

        cancellationToken.ThrowIfCancellationRequested();

        DependencyInjection.ValidateGuidNotEmpty(updateDto.Id, "Il campo Id non può essere vuoto.", nameof(updateDto.Id));
        DependencyInjection.ValidateNotNullOrWhiteSpace(updateDto.NomeOspedale, "Il nome dell'ospedale non può essere vuoto.", nameof(updateDto.NomeOspedale));
        DependencyInjection.ValidateNotNullOrWhiteSpace(updateDto.Indirizzo.Strada, "La strada dell'indirizzo non può essere vuota.", nameof(updateDto.Indirizzo.Strada));
        DependencyInjection.ValidateNotNullOrWhiteSpace(updateDto.Indirizzo.Citta, "La città dell'indirizzo non può essere vuota.", nameof(updateDto.Indirizzo.Citta));
        DependencyInjection.ValidateNotNullOrWhiteSpace(updateDto.Indirizzo.Provincia, "La provincia dell'indirizzo non può essere vuota.", nameof(updateDto.Indirizzo.Provincia));

        DependencyInjection.ValidateIntegerGreaterOrEqualThanZero(updateDto.Indirizzo.Cap, "Il CAP dell'indirizzo non può essere negativo.", nameof(updateDto.Indirizzo.Cap));

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
    /// Elimina un ospedale esistente nel database.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="KeyNotFoundException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task DeleteOspedaleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        DependencyInjection.ValidateGuidNotEmpty(id, "Il campo Id non può essere vuoto.", nameof(id));

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