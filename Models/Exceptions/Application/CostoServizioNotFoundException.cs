using System;

namespace RegistroServizi.Models.Exceptions.Application
{
    public class CostoServizioNotFoundException : Exception
    {
        public CostoServizioNotFoundException(int costoservizioId) : base($"Costo del servizio {costoservizioId} non trovato")
        {
            CostoServizioId = costoservizioId;
        }

        public int CostoServizioId { get; }
    }
}