namespace RegistroServizi.Application.Interfaces.Services;

/// <summary>
/// Defines asynchronous operations for reading and updating <c>PrezzoServizio</c> data.
/// </summary>
/// <remarks>
/// All operations accept an optional <see cref="CancellationToken"/> and return DTOs used by the application layer.
/// </remarks>
public interface IPrezzoServizioService
{
    /// <summary>
    /// Retrieves all available <c>PrezzoServizio</c> records.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>A read-only collection of <see cref="PrezzoServizioDto"/> items.</returns>
    Task<IReadOnlyList<PrezzoServizioDto>> GetAllPrezziServiziAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a <c>PrezzoServizio</c> record by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the record to retrieve.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>The matching <see cref="PrezzoServizioDto"/>.</returns>
    Task<PrezzoServizioDto> GetPrezzoServizioByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing <c>PrezzoServizio</c> record.
    /// </summary>
    /// <param name="updateDto">The update payload containing the values to apply.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>The updated <see cref="PrezzoServizioDto"/>.</returns>
    Task<PrezzoServizioDto> UpdatePrezzoServizioAsync(UpdatePrezzoServizioDto updateDto, CancellationToken cancellationToken = default);
}