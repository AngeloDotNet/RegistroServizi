using System;

namespace RegistroServizi.Models.Exceptions.Application
{
    public class ClienteNotFoundException : Exception
    {
        public ClienteNotFoundException(int clienteId) : base($"Cliente {clienteId} non trovato")
        {
            ClienteId = clienteId;
        }

        public int ClienteId { get; }
    }
}