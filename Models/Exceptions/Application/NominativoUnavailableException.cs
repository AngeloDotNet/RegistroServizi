using System;

namespace RegistroServizi.Models.Exceptions.Application
{
    public class NominativoUnavailableException : Exception
    {
        public NominativoUnavailableException(string nominativo, Exception innerException) : base($"Il nominativo del socio '{nominativo}' esiste già", innerException)
        {
            Nominativo = nominativo;
        }

        public string Nominativo { get; }
    }
}