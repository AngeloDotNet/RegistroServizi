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
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Ubicazione.Indirizzo, Title = localizer["Address"] },
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Ubicazione.Comune, Title = localizer["City"] },
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Ubicazione.Provincia, Title = localizer["Province"] },
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Ubicazione.Cap, Title = localizer["PostcodeAbbreviation"] } //PostalCodeAbbreviation
    ];

    internal static List<DataGridColumnDefinition<PrezzoServizioDto>> GetPrezziServiziColumns(IStringLocalizer<SharedResource> localizer) => [
        new DataGridColumnDefinition<PrezzoServizioDto> { Property = x => x.Id, Title = "Id", IsHidden = true },
        new DataGridColumnDefinition<PrezzoServizioDto> { Property = x => x.TipologiaServizioId, Title = "Id", IsHidden = true },
        new DataGridColumnDefinition<PrezzoServizioDto> { Property = x => x.TipoServizio, Title = "Tipologia Servizio" },
        new DataGridColumnDefinition<PrezzoServizioDto> { Property = x => x.CostoFisso, Title = "Costo Fisso", Format = GetEuroFormatTypeColumn() },
        new DataGridColumnDefinition<PrezzoServizioDto> { Property = x => x.CostoKm, Title = "Costo al Km", Format = GetEuroFormatTypeColumn() },
        new DataGridColumnDefinition<PrezzoServizioDto> { Property = x => x.SecondoTrasportato, Title = "Secondo Trasportato", Format = GetEuroFormatTypeColumn() },
        new DataGridColumnDefinition<PrezzoServizioDto> { Property = x => x.FermoMacchina, Title = "Fermo Macchina", Format = GetEuroFormatTypeColumn() },
        new DataGridColumnDefinition<PrezzoServizioDto> { Property = x => x.Accompagnatore, Title = "Accompagnatore", Format = GetEuroFormatTypeColumn() },
        new DataGridColumnDefinition<PrezzoServizioDto> { Property = x => x.ScontoSocio, Title = "Sconto Socio", Format = GetPercentFormatTypeColumn() }
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