using System.Text.Json.Serialization;

namespace LiteFM.Abstractions
{
    public class LastFMSession
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;
        [JsonPropertyName("subscriber")]
        public bool IsSubscriber { get; set; }
        [JsonIgnore]
        public bool HasLogined => !string.IsNullOrEmpty(Key);
    }
}
