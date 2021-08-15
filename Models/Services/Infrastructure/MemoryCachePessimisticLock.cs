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
        public Task ReleaseLockForMissioneAsync(int idMissione)
        {
            string key = GetKeyNameForMissione(idMissione);
            memoryCache.Remove(key);
            return Task.CompletedTask;
        }

        public Task<bool> CanLockMissioneAsync(int idMissione)
        {
            string key = GetKeyNameForMissione(idMissione);
            string currentUserId = GetCurrentUserId();
            if (!memoryCache.TryGetValue(key, out string lockerUserId))
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(lockerUserId == currentUserId);
        }

        public Task<bool> RefreshLockForMissioneAsync(int idMissione)
        {
            string key = GetKeyNameForMissione(idMissione);
            string currentUserId = GetCurrentUserId();
            TimeSpan duration = TimeSpan.FromSeconds(10);
            string userId = memoryCache.GetOrCreate(key, cacheEntry => 
            {
                cacheEntry.SetAbsoluteExpiration(duration);
                return currentUserId;
            });

            if (userId != currentUserId)
            {
                return Task.FromResult(false);
            }

            var options = new MemoryCacheEntryOptions();
            options.SetAbsoluteExpiration(duration);
            memoryCache.Set(key, currentUserId, options);
            
            
            return Task.FromResult(true);
        }

        private string GetCurrentUserId()
        {
            // TODO: Quando verrà implementato il login, restituisci l'id o il nome dell'utente loggato anziché l'id della sessione
            // return httpContextAccessor.HttpContext.User.Identity.Name;
            
            // Mantengo la sessione impostando una chiave qualsiasi
            httpContextAccessor.HttpContext.Session.SetString("foo", "bar");
            // A me in realtà interessa sapere l'id per riconoscere l'utente (IMPORTANTE: come detto, questa è una soluzione provvisoria, poi useremo l'id dell'utente loggato)
            return httpContextAccessor.HttpContext.Session.Id;
        }

        private string GetKeyNameForMissione(int idMissione)
        {
            return $"LockForMissione{idMissione}";
        }
    }
}