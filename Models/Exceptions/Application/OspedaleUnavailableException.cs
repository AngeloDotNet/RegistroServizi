using System;

namespace RegistroServizi.Models.Exceptions.Application
{
    public class OspedaleUnavailableException : Exception
    {
        public OspedaleUnavailableException(string clinica, Exception innerException) : base($"L'ospedale '{clinica}' esiste già", innerException)
        {
            Clinica = clinica;
        }

        public string Clinica { get; }
    }
}