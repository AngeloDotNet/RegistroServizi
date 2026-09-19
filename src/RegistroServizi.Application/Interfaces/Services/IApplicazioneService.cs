namespace RegistroServizi.Application.Interfaces.Services;

/// <summary>
/// Provides asynchronous operations to retrieve and update Applicazione DTOs.
/// </summary>
/// <remarks>Operations are asynchronous and accept an optional CancellationToken; implementations should observe
/// cancellation. Intended for use in the application service layer to encapsulate business logic for Applicazione
/// entities.</remarks>
public interface IApplicazioneService
{
    Task<IReadOnlyList<ApplicazioneDto>> GetAllApplicazioniAsync(CancellationToken cancellationToken = default);
    Task<ApplicazioneDto> GetByIdApplicazioneAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ApplicazioneDto> UpdateApplicazioneAsync(UpdateApplicazioneDto updateDto, CancellationToken cancellationToken = default);
}