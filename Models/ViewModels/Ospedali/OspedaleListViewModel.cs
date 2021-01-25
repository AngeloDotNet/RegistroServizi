using RegistroServizi.Models.InputModels.Ospedali;

namespace RegistroServizi.Models.ViewModels.Ospedali
{
    public class OspedaleListViewModel : IPaginationInfo
    {
        public ListViewModel<OspedaleViewModel> Ospedale { get; set; }
        public OspedaleListInputModel Input { get; set; }

        int IPaginationInfo.CurrentPage => Input.Page;
        int IPaginationInfo.TotalResults => Ospedale.TotalCount;
        int IPaginationInfo.ResultsPerPage => Input.Limit;

        string IPaginationInfo.Search => Input.Search;
        string IPaginationInfo.OrderBy => Input.OrderBy;

        bool IPaginationInfo.Ascending => Input.Ascending;
    }
}