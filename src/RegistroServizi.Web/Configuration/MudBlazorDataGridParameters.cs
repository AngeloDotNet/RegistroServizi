namespace RegistroServizi.Web.Configuration;

/// <summary>
/// Provides reusable MudBlazor data grid column definitions for the application.
/// </summary>
public static class MudBlazorDataGridParameters
{
    /// <summary>
    /// Gets the default column configuration for the hospital data grid.
    /// </summary>
    /// <remarks>
    /// The <see cref="OspedaleDto.Id"/> column is included for binding purposes but hidden from the UI.
    /// </remarks>
    internal static List<DataGridColumnDefinition<OspedaleDto>> GetOspedaliColumns(IStringLocalizer<SharedResource> localizer) => [
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Id, Title = "Id", IsHidden = true },
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.NomeOspedale, Title = localizer["Hospital"] },
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Indirizzo.Strada, Title = localizer["Address"] },
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Indirizzo.Citta, Title = localizer["City"] },
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Indirizzo.Provincia, Title = localizer["Province"] },
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Indirizzo.Cap, Title = localizer["PostcodeAbbreviation"] } //PostalCodeAbbreviation
    ];
}