using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Mobile.Services
{
    public interface ILocationService
    {
        Task<Location> GetLocationAsync();
    }
    public class LocationService : ILocationService
    {
        private readonly IGeolocation _geolocation;
        public LocationService(IGeolocation geolocation)
        {
            _geolocation = geolocation;
        }
        public async Task<Location> GetLocationAsync()
        {
            return await _geolocation.GetLocationAsync();
        }
    }
}
