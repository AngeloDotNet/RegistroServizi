namespace RegistroServizi.Application.Interfaces.Services;

/// <summary>
/// Provides asynchronous create, read, update, and delete operations for <see cref="ColonninaDto"/> values.
/// </summary>
/// <remarks>
/// All methods support <see cref="CancellationToken"/> for cooperative cancellation. Implementations should
/// validate inputs, handle not-found and concurrency scenarios, and throw appropriate exceptions for invalid
/// or failed operations. Returned DTOs represent read-only snapshots.
/// </remarks>
public interface IColonninaService
{
    /// <summary>
    /// Retrieves all available charging station DTOs.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>A read-only list containing all charging station DTOs.</returns>
    Task<IReadOnlyList<ColonninaDto>> GetAllColonnineAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a charging station DTO by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the charging station to retrieve.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>The charging station DTO that matches the specified identifier.</returns>
    Task<ColonninaDto> GetColonninaByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new charging station and returns the created DTO.
    /// </summary>
    /// <param name="createDto">The data required to create the charging station.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>The DTO representing the newly created charging station.</returns>
    Task<ColonninaDto> CreateColonninaAsync(CreateColonninaDto createDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing charging station and returns the updated DTO.
    /// </summary>
    /// <param name="updateDto">The data required to update the charging station.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>The DTO representing the updated charging station.</returns>
    Task<ColonninaDto> UpdateColonninaAsync(UpdateColonninaDto updateDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a charging station identified by the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the charging station to delete.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    Task DeleteColonninaAsync(Guid id, CancellationToken cancellationToken = default);
}