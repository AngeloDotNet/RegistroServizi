using MudBlazor;

namespace RegistroServizi.Web.Configuration;

public class MudBlazorDataGridOptions
{
    public const string SectionName = "DataGridOptions";

    public int[] PageSize { get; set; } = [5, 10, 20];
    public SortMode SortMode { get; set; } = SortMode.None;
    public bool Bordered { get; set; } = true;
    public bool Dense { get; set; } = true;
    public bool Hover { get; set; } = true;
}