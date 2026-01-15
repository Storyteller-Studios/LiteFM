using System.Text.Json.Serialization;

namespace LiteFM.Abstractions
{
    public class LastFMImage
    {
        [JsonPropertyName("size")]
        public string? Size { get; set; }
        [JsonPropertyName("#text")]
        public string? Link { get; set; }
    }
}
