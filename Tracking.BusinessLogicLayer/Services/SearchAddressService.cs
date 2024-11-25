using Newtonsoft.Json;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.BusinessLogicLayer.Services
{
    public interface ISearchAddressService
    {
        Task<AddressResponse> GetAddress(double lat, double lon);
    }
    public class SearchAddressService : ISearchAddressService
    {
        private readonly HttpClient _httpClient;
        public SearchAddressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
       
        public async Task<AddressResponse> GetAddress(double lat, double lon)
        {
            //reverse?format=json&lat=-23.519686413204617&lon=-46.59250259399414&zoom=18&addressdetails=1
            string baseUrl = $"reverse?format=json&";
            string url = $"{baseUrl}{"lat="}{lat}{"&lon="}{lon}{"&zoom=18&addressdetails=1"}";
            url = url.Replace(",", ".");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "tracking-platform");
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AddressResponse>(responseBody);
        }
    }
}
