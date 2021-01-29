using System;

namespace RegistroServizi.Models.Exceptions.Application
{
    public class SocioRinnovoNotFoundException : Exception
    {
        public SocioRinnovoNotFoundException(int id) : base($"Rinnovo {id} del socio non trovato")
        {
            Id = id;
        }

        public int Id { get; }
    }
}