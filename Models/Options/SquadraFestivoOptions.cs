namespace RegistroServizi.Models.Options
{
    public class SquadraFestivoOptions
    {
        public int PerPage { get; set; }
        public SquadraFestivoOrderOptions Order { get; set; }
    }
    
    public partial class SquadraFestivoOrderOptions
    {
        public string By { get; set; }
        public bool Ascending { get; set; }
        public string[] Allow { get; set; }
    }
}