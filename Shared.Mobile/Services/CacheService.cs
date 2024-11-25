using MonkeyCache.LiteDB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Mobile.Services
{
    public interface ICacheService
    {
        IEnumerable<T> GetCollection<T>(string key);
        bool IsExpired(string key);
        bool Exists(string key);
        void Set<T>(string key, T data, double hours);
    }
    public class CacheService : ICacheService
    {

        public CacheService()
        {
            Barrel.ApplicationId = AppInfo.Current.PackageName;
            Barrel.AutoRebuildEnabled = true;
        }
        public IEnumerable<T> GetCollection<T>(string key)
        {
            try
            {
                return Barrel.Current.Get<IEnumerable<T>>(key: key);

            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public bool IsExpired(string key)
        {
            return Barrel.Current.IsExpired(key);
        }
        public bool Exists(string key)
        {
            return Barrel.Current.Exists(key);
        }

        public void Set<T>(string key, T data, double hours)
        {
            try
            {
                if (data != null)
                {
                    if (data is IEnumerable enumerable)
                    {
                        if (enumerable.Cast<object>().Any())
                        {
                            Barrel.Current.Add<T>(key, data, TimeSpan.FromHours(hours));
                        }
                    }
                    else
                    {
                        Barrel.Current.Add<T>(key, data, TimeSpan.FromHours(hours));
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }
    }
}
