using RegistroServizi.Models.InputModels.Soci;

namespace RegistroServizi.Models.ViewModels.Soci
{
    public class SocioListViewModel : IPaginationInfo
    {
        public ListViewModel<SocioViewModel> Socio { get; set; }
        public SocioListInputModel Input { get; set; }

        int IPaginationInfo.CurrentPage => Input.Page;
        int IPaginationInfo.TotalResults => Socio.TotalCount;
        int IPaginationInfo.ResultsPerPage => Input.Limit;

        string IPaginationInfo.Search => Input.Search;
        string IPaginationInfo.OrderBy => Input.OrderBy;

        bool IPaginationInfo.Ascending => Input.Ascending;
    }
}