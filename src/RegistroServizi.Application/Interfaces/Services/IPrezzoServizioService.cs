namespace RegistroServizi.Application.Interfaces.Services;

/// <summary>
/// Provides asynchronous operations to retrieve and update PrezzoServizio DTOs.
/// </summary>
/// <remarks>All members are asynchronous and accept an optional CancellationToken. Methods return
/// PrezzoServizioDto instances or read-only collections thereof and use UpdatePrezzoServizioDto for updates.</remarks>
public interface IPrezzoServizioService
{
    Task<IReadOnlyList<PrezzoServizioDto>> GetAllPrezziServiziAsync(CancellationToken cancellationToken = default);
    Task<PrezzoServizioDto> GetByIdPrezzoServizioAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PrezzoServizioDto> UpdatePrezzoServizioAsync(UpdatePrezzoServizioDto updateDto, CancellationToken cancellationToken = default);
}