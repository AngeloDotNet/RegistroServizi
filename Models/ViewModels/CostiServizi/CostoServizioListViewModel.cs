using RegistroServizi.Models.InputModels.CostiServizi;

namespace RegistroServizi.Models.ViewModels.CostiServizi
{
    public class CostoServizioListViewModel : IPaginationInfo
    {
        public ListViewModel<CostoServizioViewModel> CostoServizio { get; set; }
        public CostoServizioListInputModel Input { get; set; }

        int IPaginationInfo.CurrentPage => Input.Page;
        int IPaginationInfo.TotalResults => CostoServizio.TotalCount;
        int IPaginationInfo.ResultsPerPage => Input.Limit;

        string IPaginationInfo.Search => Input.Search;
        string IPaginationInfo.OrderBy => Input.OrderBy;

        bool IPaginationInfo.Ascending => Input.Ascending;
    }
}