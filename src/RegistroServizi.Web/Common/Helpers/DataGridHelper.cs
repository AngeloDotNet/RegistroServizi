namespace RegistroServizi.Web.Common.Helpers;

public sealed class DataGridHelper(IStringLocalizer<SharedResource> localizer)
{
    public string CancelText => localizer["Cancel"];
    public string ConfirmText => localizer["Confirm"];
    public string RowsPerPageString => localizer["RowsPerPage"];
    public string RowsPerPageInfoFormat => localizer["RowsPerPageInfoFormat", "{first_item}", "{last_item}", "{all_items}"];
}