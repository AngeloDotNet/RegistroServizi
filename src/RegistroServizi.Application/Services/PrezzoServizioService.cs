using RegistroServizi.Application.Mapping;

namespace RegistroServizi.Application.Services;

public class PrezzoServizioService(IRegistroServiziDbContext dbContext) : IPrezzoServizioService
{
    /// <summary>
    /// Recupera tutti i prezzi dei servizi presenti nel database.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IReadOnlyList<PrezzoServizioDto>> GetAllPrezziServiziAsync(CancellationToken cancellationToken = default)
    {
        var result = await PrezzoServizioQuery()
            .OrderBy(x => x.TipologiaServizio.TipoServizio)
            .Select(prezzoServizio => PrezzoServizioMapper.MapPrezzoServizioToDto(prezzoServizio))
            .ToListAsync(cancellationToken);

        return result;
    }

    /// <summary>
    /// Recupera un prezzo di servizio specifico in base all'ID fornito.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public async Task<PrezzoServizioDto> GetByIdPrezzoServizioAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await PrezzoServizioQuery()
            .Where(x => x.Id == id)
            .Select(prezzoServizio => PrezzoServizioMapper.MapPrezzoServizioToDto(prezzoServizio))
            .FirstOrDefaultAsync(cancellationToken) ?? throw new KeyNotFoundException($"Prezzo servizio con id {id} non trovato.");

        return result;
    }

    //public async Task<PrezzoServizioDto> CreatePrezzoServizioAsync(CreatePrezzoServizioDto createDto, CancellationToken cancellationToken = default)
    //{
    //    //TODO: Validazione dei dati in ingresso (createDto) se necessario.

    //    var prezzoServizio = new PrezzoServizio
    //    {
    //        Id = Guid.NewGuid(),
    //        TipologiaServizio = createDto.TipologiaServizio,
    //        CostoFisso = createDto.CostoFisso,
    //        CostoKm = createDto.CostoKm,
    //        SecondoTrasportato = createDto.SecondoTrasportato,
    //        FermoMacchina = createDto.FermoMacchina,
    //        Accompagnatore = createDto.Accompagnatore,
    //        ScontoSocio = createDto.ScontoSocio
    //    };

    //    dbContext.PrezziServizi.Add(prezzoServizio);
    //    await dbContext.SaveChangesAsync(cancellationToken);

    //    return PrezzoServizioHelper.MapPrezzoServizioToDto(prezzoServizio);
    //}

    /// <summary>
    /// Aggiorna un prezzo di servizio esistente nel database.
    /// </summary>
    /// <param name="updateDto">Oggetto contenente i dati aggiornati del prezzo del servizio.</param>
    /// <param name="cancellationToken">Token per la cancellazione dell'operazione asincrona.</param>
    /// <returns>Il DTO del prezzo del servizio aggiornato.</returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="KeyNotFoundException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
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
        //entity.TipologiaServizioId = updateDto.TipologiaServizioId;
        //entity.CostoFisso = updateDto.CostoFisso;
        //entity.CostoKm = updateDto.CostoKm;
        //entity.SecondoTrasportato = updateDto.SecondoTrasportato;
        //entity.FermoMacchina = updateDto.FermoMacchina;
        //entity.Accompagnatore = updateDto.Accompagnatore;
        //entity.ScontoSocio = updateDto.ScontoSocio;

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

    //=> dbContext.PrezziServizi.AsNoTracking().Include(x => x.TipologiaServizio);
}