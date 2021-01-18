using System;

namespace RegistroServizi.Models.Exceptions.Application
{
    public class SocioNotFoundException : Exception
    {
        public SocioNotFoundException(int socioId) : base($"Socio {socioId} non trovato")
        {
            SocioId = socioId;
        }

        public int SocioId { get; }
    }
}