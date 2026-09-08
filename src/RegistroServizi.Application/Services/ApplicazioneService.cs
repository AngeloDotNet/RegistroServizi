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

        //if (applicazione is null)

        //{
        //    throw new NotFoundException($"Applicazione con id {id} non trovato.");
        //}

        return applicazione;
    }

    //public async Task<DetailApplicazioneDto> GetApplicazioneDetailAsync(Guid id, CancellationToken cancellationToken = default)
    //{
    //    var applicazione = await ApplicazioneQuery()
    //        .Select(applicazione => ApplicazioneHelper.MapApplicazioneToDetailDto(applicazione))
    //        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
    //        ?? throw new KeyNotFoundException($"Applicazione con id {id} non trovato.");

    //    //if (applicazione is null)
    //    //{
    //    //    throw new NotFoundException($"Applicazione con id {id} non trovato.");
    //    //}

    //    return applicazione;
    //}

    public async Task<ApplicazioneDto> CreateApplicazioneAsync(CreateApplicazioneDto createDto, CancellationToken cancellationToken = default)
    {
        // La validazione qui non è necessaria perché sono già stati validati i dati in ingresso.

        var applicazione = new Applicazione
        {
            Id = Guid.NewGuid(),
            NomeApplicazione = createDto.NomeApplicazione,
            Versione = createDto.Versione
        };

        dbContext.Applicazioni.Add(applicazione);
        await dbContext.SaveChangesAsync(cancellationToken);

        return ApplicazioneHelper.MapApplicazioneToDto(applicazione);
    }

    public async Task<ApplicazioneDto> UpdateApplicazioneAsync(UpdateApplicazioneDto updateDto, CancellationToken cancellationToken = default)
    {
        // La validazione qui non è necessaria perché sono già stati validati i dati in ingresso.

        var applicazione = await ApplicazioneQuery()
            .FirstOrDefaultAsync(x => x.Id == updateDto.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Applicazione con id {updateDto.Id} non trovato.");

        //if (applicazione is null)
        //{
        //    throw new NotFoundException($"Applicazione con id {id} non trovato.");
        //}

        applicazione.NomeApplicazione = updateDto.NomeApplicazione;
        applicazione.Versione = updateDto.Versione;

        //dbContext.Applicazioni.Update(applicazione);
        await dbContext.SaveChangesAsync(cancellationToken);

        return ApplicazioneHelper.MapApplicazioneToDto(applicazione);
    }

    private IQueryable<Applicazione> ApplicazioneQuery() => dbContext.Applicazioni.AsNoTracking();
}