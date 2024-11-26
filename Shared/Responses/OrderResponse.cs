using Newtonsoft.Json;
using System.Text.Json;


namespace Shared.Responses
{    
    public class OrderResponse : BaseResponse
    {
        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("address")]
        public string? Address { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }
}
