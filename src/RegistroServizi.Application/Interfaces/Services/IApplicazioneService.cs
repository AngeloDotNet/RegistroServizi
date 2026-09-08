namespace RegistroServizi.Application.Interfaces.Services;

public interface IApplicazioneService
{
    Task<IReadOnlyList<ApplicazioneDto>> GetAllApplicazioniAsync(CancellationToken cancellationToken = default);
    Task<ApplicazioneDto> GetByIdApplicazioneAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ApplicazioneDto> CreateApplicazioneAsync(CreateApplicazioneDto createDto, CancellationToken cancellationToken = default);
    Task<ApplicazioneDto> UpdateApplicazioneAsync(UpdateApplicazioneDto updateDto, CancellationToken cancellationToken = default);
}