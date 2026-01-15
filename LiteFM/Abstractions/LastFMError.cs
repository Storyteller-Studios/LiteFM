using LiteFM.Abstractions.Bases;
using System.Text.Json.Serialization;

namespace LiteFM.Abstractions
{
    public sealed class LastFMJsonError : LastFMErrorBase
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("error")]
        public int Error { get; set; } = 0;
    }
}
