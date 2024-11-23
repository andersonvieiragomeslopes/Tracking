using Newtonsoft.Json;
using System.Text.Json;

namespace Shared.Responses
{
    public  class BaseResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
