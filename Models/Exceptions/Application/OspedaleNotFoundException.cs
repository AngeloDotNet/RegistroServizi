using System;

namespace RegistroServizi.Models.Exceptions.Application
{
    public class OspedaleNotFoundException : Exception
    {
        public OspedaleNotFoundException(int ospedaleId) : base($"Ospedale {ospedaleId} non trovato")
        {
            OspedaleId = ospedaleId;
        }

        public int OspedaleId { get; }
    }
}