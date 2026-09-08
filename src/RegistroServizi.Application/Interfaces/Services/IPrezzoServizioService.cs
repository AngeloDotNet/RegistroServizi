namespace RegistroServizi.Application.Interfaces.Services;

public interface IPrezzoServizioService
{
    Task<IReadOnlyList<PrezzoServizioDto>> GetAllPrezziServiziAsync(CancellationToken cancellationToken = default);
    Task<PrezzoServizioDto> GetByIdPrezzoServizioAsync(Guid id, CancellationToken cancellationToken = default);
    //Task<DetailPrezzoServizioDto> GetPrezzoServizioDetailAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PrezzoServizioDto> CreatePrezzoServizioAsync(CreatePrezzoServizioDto createDto, CancellationToken cancellationToken = default);
    Task<PrezzoServizioDto> UpdatePrezzoServizioAsync(UpdatePrezzoServizioDto updateDto, CancellationToken cancellationToken = default);
}