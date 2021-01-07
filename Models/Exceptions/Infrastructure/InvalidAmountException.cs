using System;

namespace RegistroServizi.Models.Exceptions.Infrastructure
{
    public class InvalidAmountException : Exception
    {
        public InvalidAmountException() : base($"L'importo non può essere negativo")
        {
        }
    }
}