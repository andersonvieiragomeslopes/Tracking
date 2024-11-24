using Newtonsoft.Json;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.BusinessLogicLayer.Services
{
    public interface IDrivingService
    {
        Task<RouteResponse> GetRoute(PositionResponse position);
    }
    public class DrivingService : IDrivingService
    {
        private readonly HttpClient _httpClient;
        public DrivingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<RouteResponse> GetRoute(PositionResponse position)
        {
            //var toLatitude = CurrentPosition.Latitude.ToString().Replace(",", ".");
            //var toLongitude = CurrentPosition.Longitude.ToString().Replace(",", ".");
            string baseUrl = "route/v1/driving/";
            string url = $"{baseUrl}{position.Fromlongitude},{position.FromLatitude};{position.Tolongitude},{position.ToLatitude}?steps=true";

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<RouteResponse>(responseBody);
        }
    }
}
