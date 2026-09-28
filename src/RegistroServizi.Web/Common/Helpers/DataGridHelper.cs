using Microsoft.Extensions.Localization;
using RegistroServizi.Web.Resources;

namespace RegistroServizi.Web.Common.Helpers;

//public class DataGridHelper(IStringLocalizer<SharedResource> Localizer)
//{
//    public static string CancelText => Localizer["Cancel"];
//    public static string ConfirmText => Localizer["Confirm"];
//    public static string RowsPerPageString => Localizer["RowsPerPage"];
//    public static string RowsPerPageInfoFormat => Localizer["RowsPerPageInfoFormat", "{first_item}", "{last_item}", "{all_items}"];
//}
public sealed class DataGridHelper(IStringLocalizer<SharedResource> localizer)
{
    public string CancelText => localizer["Cancel"];
    public string ConfirmText => localizer["Confirm"];
    public string RowsPerPageString => localizer["RowsPerPage"];
    public string RowsPerPageInfoFormat => localizer["RowsPerPageInfoFormat", "{first_item}", "{last_item}", "{all_items}"];
}