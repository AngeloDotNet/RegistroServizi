namespace RegistroServizi.Web.Components.Shared.Miscellaneous;

public partial class ProgressNoData
{
    [Parameter] public bool IsLoading { get; set; }
    [Parameter] public int ElementsCount { get; set; }
    [Parameter] public string CountMessage { get; set; } = string.Empty;
}