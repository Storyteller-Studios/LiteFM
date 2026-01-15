using LiteFM.Abstractions.ApiContracts;
using LiteFM.Abstractions.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LiteFM.Api
{
    public static partial class LastFMApi
    {
        public static readonly ScrobbleApi ScrobbleApi = new();
    }
}

namespace LiteFM.Abstractions.ApiContracts
{
    public sealed class ScrobbleApi : LastFMContractBase<ScrobbleRequest, ScrobbleResponse, LastFMJsonError>
    {
        public override HttpRequestMessage MapRequest(LastFMOptions options, string? token, ScrobbleRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (token == null) throw new ArgumentNullException(nameof(token));
            List<KeyValuePair<string, string>> formData =
            [
                new ("artist", request.Artist),
                new ("track", request.Track),
                new ("album", request.Album),
                new ("api_key", options.ApiKey),
                new ("timestamp", request.TimeStamp.ToString()),
                new ("sk", token),
                new ("method", "track.scrobble")
            ];
            formData.Add(new("api_sig", Utils.GetLastFMAPISignature(options.ApiSecret, formData)));
            var content = new FormUrlEncodedContent(formData);
            var result = new HttpRequestMessage()
            {
                Content = content,
                Method = HttpMethod.Post,
                RequestUri = request.Endpoint
            };
            return result;
        }
    }

    public sealed class ScrobbleRequest : LastFMRequestBase
    {
        public required string Artist;
        public required string Track;
        public required string Album;
        public required uint TimeStamp;
        public readonly Uri Endpoint = new("http://ws.audioscrobbler.com/2.0/?format=json");
    }
    public sealed class ScrobbleResponse : LastFMResponseBase
    {
        [JsonPropertyName("scrobbles")]
        public ScrobblesInfo? Scrobbles { get; set; }
        public class ScrobblesInfo
        {
            [JsonPropertyName("scrobble")]
            public ScrobbleInfo? Scrobble { get; set; }
            [JsonPropertyName("@attr")]
            public ScrobbleAttributes? Attributes { get; set; }
            public class ScrobbleInfo
            {
                [JsonPropertyName("artist")]
                public CorrectedItem? Artist { get; set; }
                [JsonPropertyName("track")]
                public CorrectedItem? Track { get; set; }
                [JsonPropertyName("ignoredMessage")]
                public IgnoredMessage? IgnoredMessage { get; set; }
                [JsonPropertyName("albumArtist")]
                public CorrectedItem? AlbumArtist { get; set; }
                [JsonPropertyName("album")]
                public CorrectedItem? Album { get; set; }
                [JsonPropertyName("timestamp")]
                public int Timestamp { get; set; }
            }
            public class ScrobbleAttributes
            {
                [JsonPropertyName("ignored")]
                public int Ignored { get; set; }
                [JsonPropertyName("accepted")]
                public int Accepted { get; set; }
            }
        }
    }
}
