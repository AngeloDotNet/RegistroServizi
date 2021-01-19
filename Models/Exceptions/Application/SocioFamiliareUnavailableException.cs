using System;

namespace RegistroServizi.Models.Exceptions.Application
{
    public class SocioFamiliareUnavailableException : Exception
    {
        public SocioFamiliareUnavailableException(string familiare, Exception innerException) : base($"Il nominativo del socio familiare '{familiare}' esiste già", innerException)
        {
            Familiare = familiare;
        }

        public string Familiare { get; }
    }
}