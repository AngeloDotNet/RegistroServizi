namespace RegistroServizi.Application.Interfaces.Services;

public interface IColonninaService
{
    Task<IReadOnlyList<ColonninaDto>> GetAllColonnineAsync(CancellationToken cancellationToken = default);
    Task<ColonninaDto> GetByIdColonninaAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ColonninaDto> CreateColonninaAsync(CreateColonninaDto createDto, CancellationToken cancellationToken = default);
    Task<ColonninaDto> UpdateColonninaAsync(UpdateColonninaDto updateDto, CancellationToken cancellationToken = default);
    Task DeleteColonninaAsync(Guid id, CancellationToken cancellationToken = default);
}