namespace RegistroServizi.Models.Entities
{
     public partial class SquadraFestivo
    {
        public SquadraFestivo()
        {
            NomeSquadra = "N/A";
            DescrizioneSquadra = "N/A";
        }

        public int Id { get; private set; }
        public string NomeSquadra { get; private set; }
        public string DescrizioneSquadra { get; private set; }

        public void ChangeNomeSquadra(string newNomeSquadra)
        {
            if (string.IsNullOrWhiteSpace(newNomeSquadra))
            {
                newNomeSquadra = "N/A";
            }
            NomeSquadra = newNomeSquadra;
        }

        public void ChangeTipoServizio(string newDescrizioneSquadra)
        {
            if (string.IsNullOrWhiteSpace(newDescrizioneSquadra))
            {
                newDescrizioneSquadra = "N/A";
            }
            DescrizioneSquadra = newDescrizioneSquadra;
        }
    }
}