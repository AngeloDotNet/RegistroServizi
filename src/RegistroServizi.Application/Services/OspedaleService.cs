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

        //void ValidateNotNullOrWhiteSpace(string? value, string message, string paramName)
        //{
        //    if (string.IsNullOrWhiteSpace(value))
        //    {
        //        throw new ArgumentException(message, paramName);
        //    }
        //}

        DependencyInjection.ValidateNotNullOrWhiteSpace(createDto.NomeOspedale, "Il nome dell'ospedale non può essere vuoto.", nameof(createDto.NomeOspedale));
        DependencyInjection.ValidateNotNullOrWhiteSpace(createDto.Indirizzo.Strada, "La strada dell'indirizzo non può essere vuota.", nameof(createDto.Indirizzo.Strada));
        DependencyInjection.ValidateNotNullOrWhiteSpace(createDto.Indirizzo.Citta, "La città dell'indirizzo non può essere vuota.", nameof(createDto.Indirizzo.Citta));
        DependencyInjection.ValidateNotNullOrWhiteSpace(createDto.Indirizzo.Provincia, "La provincia dell'indirizzo non può essere vuota.", nameof(createDto.Indirizzo.Provincia));

        //if (createDto.Indirizzo.Cap == null || createDto.Indirizzo.Cap <= 0)
        //if (createDto.Indirizzo.Cap <= 0)
        //{
        //    //throw new ArgumentException("Il CAP dell'indirizzo non può essere vuoto o negativo.", nameof(createDto.Indirizzo.Cap));
        //    //throw new ArgumentException("Il CAP dell'indirizzo deve essere maggiore di zero.", nameof(createDto.Indirizzo.Cap));
        //    throw new ArgumentOutOfRangeException(nameof(createDto.Indirizzo.Cap), createDto.Indirizzo.Cap, "Il CAP dell'indirizzo non può essere negativo.");
        //}
        DependencyInjection.ValidateIntegerGreaterOrEqualThanZero(createDto.Indirizzo.Cap, "Il CAP dell'indirizzo non può essere negativo.", nameof(createDto.Indirizzo.Cap));

        //ArgumentNullException.ThrowIfNull(createDto);
        //cancellationToken.ThrowIfCancellationRequested();

        //if (string.IsNullOrWhiteSpace(createDto.NomeOspedale))
        //{
        //    throw new ArgumentException("Il nome dell'ospedale non può essere vuoto.", nameof(createDto.NomeOspedale));
        //}

        //if (string.IsNullOrWhiteSpace(createDto.Indirizzo.Strada))
        //{
        //    throw new ArgumentException("La strada dell'indirizzo non può essere vuota.", nameof(createDto.Indirizzo.Strada));
        //}

        //if (string.IsNullOrWhiteSpace(createDto.Indirizzo.Citta))
        //{
        //    throw new ArgumentException("La città dell'indirizzo non può essere vuota.", nameof(createDto.Indirizzo.Citta));
        //}

        //if (string.IsNullOrWhiteSpace(createDto.Indirizzo.Provincia))
        //{
        //    throw new ArgumentException("La provincia dell'indirizzo non può essere vuota.", nameof(createDto.Indirizzo.Provincia));
        //}

        ////if (createDto.Indirizzo.Cap == null || createDto.Indirizzo.Cap <= 0)
        //if (createDto.Indirizzo.Cap <= 0)
        //{
        //    //throw new ArgumentException("Il CAP dell'indirizzo non può essere vuoto o negativo.", nameof(createDto.Indirizzo.Cap));
        //    throw new ArgumentException("Il CAP dell'indirizzo non può essere negativo.", nameof(createDto.Indirizzo.Cap));
        //}

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
        //catch (DbUpdateConcurrencyException ex)
        //{
        //    var correlationId = Guid.NewGuid().ToString("D");
        //    throw new KeyNotFoundException($"Ospedale con id {createDto.Id} non trovato. CorrelationId: {correlationId}", ex);
        //}
        //catch (DbUpdateException ex)
        //{
        //    var correlationId = Guid.NewGuid().ToString("D");
        //    throw new InvalidOperationException($"Errore durante l'aggiornamento dell'ospedale. CorrelationId: {correlationId}", ex);
        //}

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

        //if (updateDto.Id == Guid.Empty)
        //{
        //    throw new ArgumentException("Il campo Id non può essere vuoto.", nameof(updateDto.Id));
        //}

        //static void ThrowIfNullOrWhiteSpace(string value, string paramName, string message)
        //{
        //    if (string.IsNullOrWhiteSpace(value))
        //    {
        //        throw new ArgumentException(message, paramName);
        //    }
        //}

        DependencyInjection.ValidateGuidNotEmpty(updateDto.Id, "Il campo Id non può essere vuoto.", nameof(updateDto.Id));
        DependencyInjection.ValidateNotNullOrWhiteSpace(updateDto.NomeOspedale, "Il nome dell'ospedale non può essere vuoto.", nameof(updateDto.NomeOspedale));
        DependencyInjection.ValidateNotNullOrWhiteSpace(updateDto.Indirizzo.Strada, "La strada dell'indirizzo non può essere vuota.", nameof(updateDto.Indirizzo.Strada));
        DependencyInjection.ValidateNotNullOrWhiteSpace(updateDto.Indirizzo.Citta, "La città dell'indirizzo non può essere vuota.", nameof(updateDto.Indirizzo.Citta));
        DependencyInjection.ValidateNotNullOrWhiteSpace(updateDto.Indirizzo.Provincia, "La provincia dell'indirizzo non può essere vuota.", nameof(updateDto.Indirizzo.Provincia));
        //ThrowIfNullOrWhiteSpace(updateDto.NomeOspedale, nameof(updateDto.NomeOspedale), "Il campo NomeOspedale non può essere nullo o vuoto.");
        //ThrowIfNullOrWhiteSpace(updateDto.Indirizzo.Strada, nameof(updateDto.Indirizzo.Strada), "La strada dell'indirizzo non può essere vuota.");
        //ThrowIfNullOrWhiteSpace(updateDto.Indirizzo.Citta, nameof(updateDto.Indirizzo.Citta), "La città dell'indirizzo non può essere vuota.");
        //ThrowIfNullOrWhiteSpace(updateDto.Indirizzo.Provincia, nameof(updateDto.Indirizzo.Provincia), "La provincia dell'indirizzo non può essere vuota.");

        //if (updateDto.Indirizzo.Cap == null || updateDto.Indirizzo.Cap <= 0)
        //if (updateDto.Indirizzo.Cap <= 0)
        //{
        //    //throw new ArgumentException("Il CAP dell'indirizzo non può essere vuoto o negativo.", nameof(updateDto.Indirizzo.Cap));
        //    throw new ArgumentOutOfRangeException(nameof(updateDto.Indirizzo.Cap), updateDto.Indirizzo.Cap, "Il CAP dell'indirizzo non può essere negativo.");
        //}
        DependencyInjection.ValidateIntegerGreaterOrEqualThanZero(updateDto.Indirizzo.Cap, "Il CAP dell'indirizzo non può essere negativo.", nameof(updateDto.Indirizzo.Cap));

        //if (updateDto.Id == Guid.Empty)
        //{
        //    throw new ArgumentException("Il campo Id non può essere vuoto.", nameof(updateDto.Id));
        //}

        //if (string.IsNullOrWhiteSpace(updateDto.NomeOspedale))
        //{
        //    throw new ArgumentNullException(nameof(updateDto.NomeOspedale), "Il campo NomeOspedale non può essere nullo o vuoto.");
        //}

        //if (string.IsNullOrWhiteSpace(updateDto.Indirizzo.Strada))
        //{
        //    throw new ArgumentException("La strada dell'indirizzo non può essere vuota.", nameof(updateDto.Indirizzo.Strada));
        //}

        //if (string.IsNullOrWhiteSpace(updateDto.Indirizzo.Citta))
        //{
        //    throw new ArgumentException("La città dell'indirizzo non può essere vuota.", nameof(updateDto.Indirizzo.Citta));
        //}

        //if (string.IsNullOrWhiteSpace(updateDto.Indirizzo.Provincia))
        //{
        //    throw new ArgumentException("La provincia dell'indirizzo non può essere vuota.", nameof(updateDto.Indirizzo.Provincia));
        //}

        ////if (updateDto.Indirizzo.Cap == null || updateDto.Indirizzo.Cap <= 0)
        //if (updateDto.Indirizzo.Cap <= 0)
        //{
        //    //throw new ArgumentException("Il CAP dell'indirizzo non può essere vuoto o negativo.", nameof(updateDto.Indirizzo.Cap));
        //    throw new ArgumentException("Il CAP dell'indirizzo non può essere negativo.", nameof(updateDto.Indirizzo.Cap));
        //}

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
        //if (id == Guid.Empty)
        //{
        //    throw new ArgumentException("Il campo Id non può essere vuoto.", nameof(id));
        //}
        DependencyInjection.ValidateGuidNotEmpty(id, "Il campo Id non può essere vuoto.", nameof(id));

        var entity = await dbContext.Ospedali.FindAsync([id], cancellationToken).ConfigureAwait(false)
            ?? throw new KeyNotFoundException($"Ospedale con id {id} non trovato.");

        dbContext.Ospedali.Remove(entity);

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            //dbContext.Entry(entity).State = EntityState.Detached;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            var correlationId = Guid.NewGuid().ToString("D");
            throw new KeyNotFoundException($"Ospedale con id {id} non trovato. CorrelationId: {correlationId}", ex);
        }
        catch (DbUpdateException ex)
        {
            var correlationId = Guid.NewGuid().ToString("D");
            throw new InvalidOperationException($"Errore durante l'eliminazione dell'ospedale. CorrelationId: {correlationId}", ex);
        }
    }

    private IQueryable<Ospedale> OspedaleQuery() => dbContext.Ospedali.AsNoTracking();
}