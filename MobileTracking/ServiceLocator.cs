using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileTracking
{
    public class ServiceLocator : IDisposable
    {
        static private readonly ConcurrentDictionary<int, ServiceLocator> _serviceLocators = new ConcurrentDictionary<int, ServiceLocator>();

        static private ServiceProvider _rootServiceProvider = null;
        private bool _isInitialized;

        public bool IsInitialized => _isInitialized;
        private IServiceScope _serviceScope = null;

        static public ServiceLocator Instance
        {
            get
            {
                return _serviceLocators.GetOrAdd(1, new ServiceLocator());
            }
        }

        public ServiceLocator()
        {
            _serviceScope = _rootServiceProvider.CreateScope();
        }

        static public void Configure(IServiceCollection serviceCollections)
        {
            _rootServiceProvider = serviceCollections.BuildServiceProvider();

        }

        public T Resolve<T>(bool isRequired = true)
        {
            if (isRequired)
            {
                return _serviceScope.ServiceProvider.GetRequiredService<T>();
            }

            return _serviceScope.ServiceProvider.GetService<T>();
        }

        public object GetService(Type type)
        {
            return _serviceScope.ServiceProvider.GetService(type);
        }
        public void Init()
        {
            if (_isInitialized) throw new InvalidOperationException("ServiceLocator is already initialized");
            _isInitialized = true;
        }

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_serviceScope != null)
                {
                    _serviceScope.Dispose();
                }
            }
        }
        #endregion
    }
}

