using System.Text.Json.Serialization;

namespace LiteFM.Abstractions
{
    public class CorrectedItem
    {
        [JsonPropertyName("corrected")]
        public bool? Corrected { get; set; }
        [JsonPropertyName("#text")]
        public string? Text { get; set; }
    }
}
