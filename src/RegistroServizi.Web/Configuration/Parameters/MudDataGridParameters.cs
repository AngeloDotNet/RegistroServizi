namespace RegistroServizi.Web.Configuration.Parameters;

/// <summary>
/// Provides reusable column and formatting definitions for MudBlazor data grids.
/// </summary>
/// <remarks>
/// Centralizing grid configuration keeps column ordering, visibility, localization, and numeric
/// formatting consistent across the Blazor application. The returned definitions can be supplied
/// to the corresponding grid components when rendering hospital and service-pricing data.
/// <para>
/// For additional information about MudBlazor data grids, see the
/// <see href="https://mudblazor.com/components/datagrid">MudBlazor data grid documentation</see>.
/// </para>
/// </remarks>
public static class MudDataGridParameters
{
    /// <summary>
    /// Creates the standard column definitions for the hospital data grid.
    /// </summary>
    /// <param name="localizer">
    /// The <see cref="IStringLocalizer{SharedResource}"/> used to resolve localized column titles.
    /// </param>
    /// <returns>
    /// A list of <see cref="DataGridColumnDefinition{T}"/> entries that define the hospital grid
    /// columns and their display order.
    /// </returns>
    /// <remarks>
    /// The <see cref="OspedaleDto.Id"/> column is included so the grid can identify and bind each
    /// row, but it is hidden because the identifier is not intended for user display.
    /// Address-related titles are resolved through the shared localization resource so the grid
    /// can adapt to the current UI culture.
    /// </remarks>
    internal static List<DataGridColumnDefinition<OspedaleDto>> GetOspedaliColumns(IStringLocalizer<SharedResource> localizer) => [
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Id, Title = "Id", IsHidden = true },
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.NomeOspedale, Title = localizer["Hospital"] },
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Ubicazione.Indirizzo, Title = localizer["Address"] },
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Ubicazione.Comune, Title = localizer["City"] },
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Ubicazione.Provincia, Title = localizer["Province"] },
        new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Ubicazione.Cap, Title = localizer["PostcodeAbbreviation"] }
    ];

    /// <summary>
    /// Creates the standard column definitions for the service-pricing data grid.
    /// </summary>
    /// <param name="localizer">
    /// The localizer reserved for resolving localized service-pricing column titles.
    /// </param>
    /// <returns>
    /// A list of <see cref="DataGridColumnDefinition{T}"/> entries that define the service-pricing
    /// grid columns and their display order.
    /// </returns>
    /// <remarks>
    /// The service and pricing identifiers are included for binding and row identification, but
    /// they are hidden from the user interface. Monetary values use the <c>C2</c> currency format,
    /// while discounts use the <c>P2</c> percentage format.
    /// <para>
    /// The <paramref name="localizer"/> parameter is kept in the method signature so localized
    /// service-pricing titles can be introduced without changing the grid configuration API.
    /// </para>
    /// </remarks>
    internal static List<DataGridColumnDefinition<PrezzoServizioDto>> GetPrezziServiziColumns(IStringLocalizer<SharedResource> localizer) => [
        new DataGridColumnDefinition<PrezzoServizioDto> { Property = x => x.Id, Title = "Id", IsHidden = true },
        new DataGridColumnDefinition<PrezzoServizioDto> { Property = x => x.TipologiaServizioId, Title = "Id", IsHidden = true },
        new DataGridColumnDefinition<PrezzoServizioDto> { Property = x => x.TipoServizio, Title = "Tipologia Servizio" },
        new DataGridColumnDefinition<PrezzoServizioDto> { Property = x => x.CostoFisso, Title = "Costo Fisso", Format = GetEuroFormatTypeColumn() },
        new DataGridColumnDefinition<PrezzoServizioDto> { Property = x => x.CostoKm, Title = "Costo al Km", Format = GetEuroFormatTypeColumn() },
        new DataGridColumnDefinition<PrezzoServizioDto> { Property = x => x.SecondoTrasportato, Title = "Secondo Trasportato", Format = GetEuroFormatTypeColumn() },
        new DataGridColumnDefinition<PrezzoServizioDto> { Property = x => x.FermoMacchina, Title = "Fermo Macchina", Format = GetEuroFormatTypeColumn() },
        new DataGridColumnDefinition<PrezzoServizioDto> { Property = x => x.Accompagnatore ?? 0m, Title = "Accompagnatore", Format = GetEuroFormatTypeColumn() },
        new DataGridColumnDefinition<PrezzoServizioDto> { Property = x => x.ScontoSocio ?? 0, Title = "Sconto Socio", Format = GetPercentFormatTypeColumn() }
    ];

    /// <summary>
    /// Creates the column definitions used to render the charging stations grid, including localized headers for user-facing fields.
    /// </summary>
    /// <param name="localizer">Localizer used to resolve the translated column titles.</param>
    /// <returns>A list of <see cref="DataGridColumnDefinition{ColonninaDto}" /> values defining the Id, charging station name, city, and province columns.</returns>
    internal static List<DataGridColumnDefinition<ColonninaDto>> GetColonnineColumns(IStringLocalizer<SharedResource> localizer) => [
        new DataGridColumnDefinition<ColonninaDto> { Property = x => x.Id, Title = "Id", IsHidden = true },
        new DataGridColumnDefinition<ColonninaDto> { Property = x => x.NomeColonnina, Title = localizer["ChargingStation"] },
        new DataGridColumnDefinition<ColonninaDto> { Property = x => x.Comune, Title = localizer["City"] },
        new DataGridColumnDefinition<ColonninaDto> { Property = x => x.Provincia, Title = localizer["Province"] },
    ];

    /// <summary>
    /// Gets the standard numeric format specifier for euro currency values.
    /// </summary>
    /// <returns>
    /// The <c>C2</c> standard numeric format specifier, which displays a currency symbol and
    /// two decimal places according to the current culture.
    /// </returns>
    /// <remarks>
    /// The format is applied by the grid when rendering monetary values, ensuring consistent
    /// presentation of service prices.
    /// </remarks>
    internal static string GetEuroFormatTypeColumn() => "C2";

    /// <summary>
    /// Gets the standard numeric format specifier for percentage values.
    /// </summary>
    /// <returns>
    /// The <c>P2</c> standard numeric format specifier, which displays a percentage with two
    /// decimal places according to the current culture.
    /// </returns>
    /// <remarks>
    /// The format is applied by the grid when rendering member discounts.
    /// </remarks>
    internal static string GetPercentFormatTypeColumn() => "P2";
}