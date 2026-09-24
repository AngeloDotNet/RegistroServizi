namespace RegistroServizi.Application.Interfaces.Services;

/// <summary>
/// Provides asynchronous create, read, update, and delete operations for Colonnina data transfer objects (DTOs).
/// </summary>
/// <remarks>All methods support CancellationToken for cooperative cancellation. Implementations should validate
/// inputs, handle not-found and concurrency scenarios, and throw appropriate exceptions for invalid or failed
/// operations. Returned DTOs represent read-only snapshots.</remarks>
public interface IColonninaService
{
    Task<IReadOnlyList<ColonninaDto>> GetAllColonnineAsync(CancellationToken cancellationToken = default);
    Task<ColonninaDto> GetByIdColonninaAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ColonninaDto> CreateColonninaAsync(CreateColonninaDto createDto, CancellationToken cancellationToken = default);
    Task<ColonninaDto> UpdateColonninaAsync(UpdateColonninaDto updateDto, CancellationToken cancellationToken = default);
    Task DeleteColonninaAsync(Guid id, CancellationToken cancellationToken = default);
}