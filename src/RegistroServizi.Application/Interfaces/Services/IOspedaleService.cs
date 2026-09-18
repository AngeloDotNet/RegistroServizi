namespace RegistroServizi.Application.Interfaces.Services;

public interface IOspedaleService
{
    Task<IReadOnlyList<OspedaleDto>> GetAllOspedaliAsync(CancellationToken cancellationToken = default);
    Task<OspedaleDto> GetByIdOspedaleAsync(Guid id, CancellationToken cancellationToken = default);
    Task<OspedaleDto> CreateOspedaleAsync(CreateOspedaleDto createDto, CancellationToken cancellationToken = default);
    Task<OspedaleDto> UpdateOspedaleAsync(UpdateOspedaleDto updateDto, CancellationToken cancellationToken = default);
    Task DeleteOspedaleAsync(Guid id, CancellationToken cancellationToken = default);
}