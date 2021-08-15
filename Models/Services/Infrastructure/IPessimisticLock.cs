using System.Threading.Tasks;

namespace RegistroServizi.Models.Services.Infrastructure
{
    public interface IPessimisticLock
    {
        Task<bool> TryAcquireLockForMissione(int idMissione);
        Task ReleaseLockForMissione(int idMissione);
    }
}