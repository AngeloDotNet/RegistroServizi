namespace RegistroServizi.Models.ViewModels.SociRinnovi
{
    public class SocioRinnovoDetailViewModel
    {
        public int Id { get; set; }
        public int SocioId { get; set; }
        public string Anno { get; set; }
        public string Quota { get; set; }
        public string DataRinnovo { get; set; }
    }
}