using System;
using System.Text.Json.Serialization;

namespace LiteFM.Abstractions
{
    public class LastFMUser
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("age")]
        public uint Age { get; set; }
        [JsonPropertyName("subscriber")]
        public bool IsSubscriber { get; set; }
        [JsonPropertyName("realname")]
        public string? RealName { get; set; }
        [JsonPropertyName("bootstrap")]
        public int Bootstrap { get; set; }
        [JsonPropertyName("playcount")]
        public uint PlayCount { get; set; }
        [JsonPropertyName("artist_count")]
        public uint ArtistCount { get; set; }
        [JsonPropertyName("playlists")]
        public uint Playlists { get; set; }
        [JsonPropertyName("track_count")]
        public uint TrackCount { get; set; }
        [JsonPropertyName("album_count")]
        public uint AlbumCount { get; set; }
        [JsonPropertyName("image")]
        public LastFMImage[]? Image { get; set; }
        [JsonPropertyName("registered")]
        public RegisterTime? UserRegisterTime { get; set; }
        [JsonPropertyName("country")]
        public string? Country { get; set; }
        [JsonPropertyName("gender")]
        public string? Gender { get; set; }
        [JsonPropertyName("url")]
        public string? Url { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        public class RegisterTime
        {
            [JsonPropertyName("unixtime")]
            public uint UnixTime { get; set; } = 0;

#if NET
            public DateTime DateTime => DateTime.UnixEpoch.AddSeconds(UnixTime);
#endif
#if NETSTANDARD
            public DateTime DateTime => Utils.UnixEpoch.AddSeconds(UnixTime);
#endif
        }
    }
}
