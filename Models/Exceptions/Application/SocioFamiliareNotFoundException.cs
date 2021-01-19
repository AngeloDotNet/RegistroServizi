using System;

namespace RegistroServizi.Models.Exceptions.Application
{
    public class SocioFamiliareNotFoundException : Exception
    {
        public SocioFamiliareNotFoundException(int id) : base($"Socio familiare {id} non trovato")
        {
            Id = id;
        }

        public int Id { get; }
    }
}