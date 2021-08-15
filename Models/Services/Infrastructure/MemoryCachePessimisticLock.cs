using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace RegistroServizi.Models.Services.Infrastructure
{
    public class MemoryCachePessimisticLock : IPessimisticLock
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMemoryCache memoryCache;

        public MemoryCachePessimisticLock(IHttpContextAccessor httpContextAccessor, IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
            this.httpContextAccessor = httpContextAccessor;
        }

        // TODO: Questi metodi restituiscono un Task nel caso volessi renderli asincroni
        // es. per salvare il lock nel database
        public Task ReleaseLockForMissione(int idMissione)
        {
            string key = GetKeyNameForMissione(idMissione);
            memoryCache.Remove(key);
            return Task.CompletedTask;
        }

        public Task<bool> TryAcquireLockForMissione(int idMissione)
        {
            string key = GetKeyNameForMissione(idMissione);
            string currentUserId = GetCurrentUserId();
            string userId = memoryCache.GetOrCreate(key, cacheEntry => 
            {
                cacheEntry.SetSlidingExpiration(TimeSpan.FromSeconds(10));
                return currentUserId;
            });
            
            return Task.FromResult(userId == currentUserId);
        }

        private string GetCurrentUserId()
        {
            // TODO: Quando verrà implementato il login, restituisci l'id o il nome dell'utente loggato anziché l'id della sessione
            // return httpContextAccessor.HttpContext.User.Identity.Name;
            return httpContextAccessor.HttpContext.Session.Id;
        }

        private string GetKeyNameForMissione(int idMissione)
        {
            return $"LockForMissione{idMissione}";
        }
    }
}