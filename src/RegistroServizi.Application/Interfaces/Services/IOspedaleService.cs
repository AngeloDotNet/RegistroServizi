namespace RegistroServizi.Application.Interfaces.Services;

/// <summary>
/// Defines asynchronous CRUD operations for hospital records represented by <see cref="RegistroServizi.Application.DTOs.Ospedale.OspedaleDto"/>.
/// </summary>
/// <remarks>
/// Implementations should validate input data, honor cancellation requests, and map between DTOs and persistence models.
/// </remarks>
public interface IOspedaleService
{
    /// <summary>
    /// Gets all hospitals.
    /// </summary>
    /// <remarks>
    /// The returned collection should contain all available hospitals, typically ordered by identifier.
    /// </remarks>
    /// <param name="cancellationToken">A token to observe while the operation is in progress.</param>
    /// <returns>A read-only list of <see cref="RegistroServizi.Application.DTOs.Ospedale.OspedaleDto"/> instances.</returns>
    Task<IReadOnlyList<OspedaleDto>> GetAllOspedaliAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a hospital by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the hospital to retrieve.</param>
    /// <param name="cancellationToken">A token to observe while the operation is in progress.</param>
    /// <returns>The matching <see cref="RegistroServizi.Application.DTOs.Ospedale.OspedaleDto"/>.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when no hospital exists with the specified identifier.</exception>
    Task<OspedaleDto> GetOspedaleByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new hospital.
    /// </summary>
    /// <param name="createDto">The data required to create the hospital.</param>
    /// <param name="cancellationToken">A token to observe while the operation is in progress.</param>
    /// <returns>The created <see cref="RegistroServizi.Application.DTOs.Ospedale.OspedaleDto"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="createDto"/> or its nested address data is <see langword="null"/>.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the hospital cannot be persisted.</exception>
    Task<OspedaleDto> CreateOspedaleAsync(CreateOspedaleDto createDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing hospital.
    /// </summary>
    /// <param name="updateDto">The data required to update the hospital.</param>
    /// <param name="cancellationToken">A token to observe while the operation is in progress.</param>
    /// <returns>The updated <see cref="RegistroServizi.Application.DTOs.Ospedale.OspedaleDto"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="updateDto"/> or its nested address data is <see langword="null"/>.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when no hospital exists with the specified identifier.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the update cannot be persisted.</exception>
    Task<OspedaleDto> UpdateOspedaleAsync(UpdateOspedaleDto updateDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a hospital by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the hospital to delete.</param>
    /// <param name="cancellationToken">A token to observe while the operation is in progress.</param>
    /// <exception cref="KeyNotFoundException">Thrown when no hospital exists with the specified identifier.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the delete operation cannot be persisted.</exception>
    Task DeleteOspedaleAsync(Guid id, CancellationToken cancellationToken = default);
}