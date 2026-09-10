namespace RegistroServizi.Application.Services;

public class ApplicazioneService(IRegistroServiziDbContext dbContext) : IApplicazioneService
{
    public async Task<IReadOnlyList<ApplicazioneDto>> GetAllApplicazioniAsync(CancellationToken cancellationToken = default)
    {
        var applicazioni = await ApplicazioneQuery()
            .OrderBy(x => x.Id)
            .Select(applicazione => ApplicazioneHelper.MapApplicazioneToDto(applicazione))
            .ToListAsync(cancellationToken);

        return applicazioni;
    }

    public async Task<ApplicazioneDto> GetByIdApplicazioneAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var applicazione = await ApplicazioneQuery()
            .Select(applicazione => ApplicazioneHelper.MapApplicazioneToDto(applicazione))
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            ?? throw new KeyNotFoundException($"Applicazione con id {id} non trovato.");

        return applicazione;
    }

    //public async Task<ApplicazioneDto> CreateApplicazioneAsync(CreateApplicazioneDto createDto, CancellationToken cancellationToken = default)
    //{
    //    //TODO: Validazione dei dati in ingresso (createDto) se necessario.

    //    var applicazione = new Applicazione
    //    {
    //        Id = Guid.NewGuid(),
    //        NomeApplicazione = createDto.NomeApplicazione,
    //        Versione = createDto.Versione
    //    };

    //    dbContext.Applicazioni.Add(applicazione);
    //    await dbContext.SaveChangesAsync(cancellationToken);

    //    return ApplicazioneHelper.MapApplicazioneToDto(applicazione);
    //}

    public async Task<ApplicazioneDto> UpdateApplicazioneAsync(UpdateApplicazioneDto updateDto, CancellationToken cancellationToken = default)
    {
        //TODO: Validazione dei dati in ingresso (updateDto) se necessario.

        var local = dbContext.Applicazioni.Local.FirstOrDefault(x => x.Id == updateDto.Id);

        if (local is not null)
        {
            dbContext.Entry(local).State = EntityState.Detached;
        }

        var applicazione = await dbContext.Applicazioni
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == updateDto.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Applicazione con id {updateDto.Id} non trovato.");

        applicazione.NomeApplicazione = updateDto.NomeApplicazione;
        applicazione.Versione = updateDto.Versione;

        dbContext.Applicazioni.Attach(applicazione);
        dbContext.Entry(applicazione).State = EntityState.Modified;

        await dbContext.SaveChangesAsync(cancellationToken);

        return ApplicazioneHelper.MapApplicazioneToDto(applicazione);
    }

    private IQueryable<Applicazione> ApplicazioneQuery() => dbContext.Applicazioni.AsNoTracking();
}