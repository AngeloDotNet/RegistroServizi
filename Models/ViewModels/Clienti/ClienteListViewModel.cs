using RegistroServizi.Models.InputModels.Clienti;

namespace RegistroServizi.Models.ViewModels.Clienti
{
    public class ClienteListViewModel : IPaginationInfo
    {
        public ListViewModel<ClienteViewModel> Cliente { get; set; }
        public ClienteListInputModel Input { get; set; }

        int IPaginationInfo.CurrentPage => Input.Page;
        int IPaginationInfo.TotalResults => Cliente.TotalCount;
        int IPaginationInfo.ResultsPerPage => Input.Limit;

        string IPaginationInfo.Search => Input.Search;
        string IPaginationInfo.OrderBy => Input.OrderBy;

        bool IPaginationInfo.Ascending => Input.Ascending;
    }
}