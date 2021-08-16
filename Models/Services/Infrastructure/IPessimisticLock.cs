using System.Threading.Tasks;

namespace RegistroServizi.Models.Services.Infrastructure
{
    public interface IPessimisticLock
    {
        Task<bool> CanLockMissioneAsync(int idMissione);
        Task<bool> RefreshLockForMissioneAsync(int idMissione);
        Task ReleaseLockForMissioneAsync(int idMissione);
    }
}