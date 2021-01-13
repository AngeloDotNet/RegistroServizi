using System;

namespace RegistroServizi.Models.Exceptions.Application
{
    public class RagioneSocialeUnavailableException : Exception
    {
        public RagioneSocialeUnavailableException(string ragionesociale, Exception innerException) : base($"La ragione sociale '{ragionesociale}' esiste già", innerException)
        {
            RagioneSociale = ragionesociale;
        }

        public string RagioneSociale { get; }
    }
}