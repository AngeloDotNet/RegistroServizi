using System;

namespace RegistroServizi.Models.Exceptions.Application
{
    public class PessimisticLockAcquireException : Exception
    {
        public PessimisticLockAcquireException() : base ("Couldn't get a pessimistic lock on resource")
        {
        }
    }
}