namespace RegistroServizi.Web.Configuration;

/// <summary>
/// Provides reusable configuration helpers for MudBlazor data grids.
/// </summary>
/// <remarks>
/// This class centralizes column definitions and standard format strings so grid layouts remain consistent
/// across the application.
/// </remarks>
public static class MudBlazorDataGridParameters
{
    /// <summary>
    /// Gets the default column definitions for the hospital data grid.
    /// </summary>
    /// <param name="localizer">
    /// The localizer used to resolve user-facing column titles.
    /// </param>
    /// <returns>
    /// A list of <see cref="DataGridColumnDefinition{T}"/> entries describing the hospital grid columns.
    /// </returns>
    /// <remarks>
    /// The <see cref="OspedaleDto.Id"/> column is included for data binding and identification purposes,
    /// but it is hidden from the UI.
    /// </remarks>
    internal static List<DataGridColumnDefinition<OspedaleDto>> GetOspedaliColumns(IStringLocalizer<SharedResource> localizer) => [
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Id, Title = "Id", IsHidden = true },
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.NomeOspedale, Title = localizer["Hospital"] },
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Indirizzo.Strada, Title = localizer["Address"] },
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Indirizzo.Citta, Title = localizer["City"] },
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Indirizzo.Provincia, Title = localizer["Province"] },
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Indirizzo.Cap, Title = localizer["PostcodeAbbreviation"] } //PostalCodeAbbreviation
    ];

    /// <summary>
    /// Gets the standard numeric format string for euro currency values.
    /// </summary>
    /// <returns>The .NET composite format string <c>C2</c>.</returns>
    internal static string GetEuroFormatTypeColumn() => "C2";

    /// <summary>
    /// Gets the standard numeric format string for percentage values.
    /// </summary>
    /// <returns>The .NET composite format string <c>P2</c>.</returns>
    internal static string GetPercentFormatTypeColumn() => "P2";
}