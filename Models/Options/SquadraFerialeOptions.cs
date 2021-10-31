namespace RegistroServizi.Models.Options
{
    public class SquadraFerialeOptions
    {
        public int PerPage { get; set; }
        public SquadraFerialeOrderOptions Order { get; set; }
    }
    
    public partial class SquadraFerialeOrderOptions
    {
        public string By { get; set; }
        public bool Ascending { get; set; }
        public string[] Allow { get; set; }
    }
}