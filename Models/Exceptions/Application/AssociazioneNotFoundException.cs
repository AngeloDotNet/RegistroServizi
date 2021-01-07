using System;

namespace RegistroServizi.Models.Exceptions.Application
{
    public class AssociazioneNotFoundException : Exception
    {
        public AssociazioneNotFoundException(int associazioneId) : base($"Associazione {associazioneId} non trovata")
        {
            AssociazioneId = associazioneId;
        }

        public int AssociazioneId { get; }
    }
}