using System.Text.Json.Serialization;

namespace ContactManagement.API.Models
{
    public class SearchParams
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }
        [JsonPropertyName("size")]
        public int Size { get; set; }
        [JsonPropertyName("search")]
        public string? Search { get; set; }
    }
}
