namespace RegistroServizi.Application.Interfaces.Services;

/// <summary>
/// Provides asynchronous operations to retrieve, create, update, and delete Ospedale entities represented by
/// OspedaleDto.
/// </summary>
/// <remarks>All methods are asynchronous and accept an optional CancellationToken. Implementations are intended
/// for dependency injection and should handle validation, mapping, and concurrency concerns.</remarks>
public interface IOspedaleService
{
    Task<IReadOnlyList<OspedaleDto>> GetAllOspedaliAsync(CancellationToken cancellationToken = default);
    Task<OspedaleDto> GetOspedaleByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<OspedaleDto> CreateOspedaleAsync(CreateOspedaleDto createDto, CancellationToken cancellationToken = default);
    Task<OspedaleDto> UpdateOspedaleAsync(UpdateOspedaleDto updateDto, CancellationToken cancellationToken = default);
    Task DeleteOspedaleAsync(Guid id, CancellationToken cancellationToken = default);
}