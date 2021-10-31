using RegistroServizi.Models.InputModels.SquadreFestive;

namespace RegistroServizi.Models.ViewModels.SquadreFestive
{
    public class SquadraFestivoListViewModel : IPaginationInfo
    {
        public ListViewModel<SquadraFestivoViewModel> Socio { get; set; }
        public SquadraFestivoListInputModel Input { get; set; }

        int IPaginationInfo.CurrentPage => Input.Page;
        int IPaginationInfo.TotalResults => Socio.TotalCount;
        int IPaginationInfo.ResultsPerPage => Input.Limit;

        string IPaginationInfo.Search => Input.Search;
        string IPaginationInfo.OrderBy => Input.OrderBy;

        bool IPaginationInfo.Ascending => Input.Ascending;
    }
}