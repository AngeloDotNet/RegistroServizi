namespace RegistroServizi.Web.Components.Shared.DataGrids;

public partial class DataGridPager<TItem>
{
    [Parameter, EditorRequired] public MudBlazorDataGridOptions DataGridOptions { get; set; } = default!;

    [Parameter] public string RowsPerPageString { get; set; } = string.Empty;
    [Parameter] public string InfoFormat { get; set; } = string.Empty;

    [Parameter] public int[]? PageSizeOptions { get; set; }

    [Parameter] public bool PageSizeSelector { get; set; } = true;
    [Parameter] public bool ShowNavigation { get; set; } = true;
    [Parameter] public bool ShowPageNumber { get; set; } = false;

    private int[] EffectivePageSizeOptions => PageSizeOptions ?? DataGridOptions.PageSize;
}