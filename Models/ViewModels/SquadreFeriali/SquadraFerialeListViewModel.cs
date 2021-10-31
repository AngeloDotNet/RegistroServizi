using RegistroServizi.Models.InputModels.SquadreFeriali;

namespace RegistroServizi.Models.ViewModels.SquadreFeriali
{
    public class SquadraFerialeListViewModel : IPaginationInfo
    {
        public ListViewModel<SquadraFerialeViewModel> Socio { get; set; }
        public SquadraFerialeListInputModel Input { get; set; }

        int IPaginationInfo.CurrentPage => Input.Page;
        int IPaginationInfo.TotalResults => Socio.TotalCount;
        int IPaginationInfo.ResultsPerPage => Input.Limit;

        string IPaginationInfo.Search => Input.Search;
        string IPaginationInfo.OrderBy => Input.OrderBy;

        bool IPaginationInfo.Ascending => Input.Ascending;
    }
}