namespace RegistroServizi.Web.Components.Shared;

public partial class DataGridPager
{
    [Parameter, EditorRequired] public MudBlazorDataGridOptions DataGridOptions { get; set; } = default!;

    [Parameter] public string RowsPerPageString { get; set; } = "Righe per pagina:";
    [Parameter] public string InfoFormat { get; set; } = "Righe {first_item} - {last_item} di {all_items}";

    [Parameter] public int[]? PageSizeOptions { get; set; }

    [Parameter] public bool PageSizeSelector { get; set; } = true;
    [Parameter] public bool ShowNavigation { get; set; } = true;
    [Parameter] public bool ShowPageNumber { get; set; } = false;

    private int[] EffectivePageSizeOptions => PageSizeOptions ?? DataGridOptions.PageSize;
}