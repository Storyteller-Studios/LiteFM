using System.Text.Json.Serialization;

namespace LiteFM.Abstractions.Bases
{
    public class LastFMErrorBase : LastFMResponseBase
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("error")]
        public int Error { get; set; } = 0;
    }
}
