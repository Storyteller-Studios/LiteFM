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
        public static readonly UpdateNowPlayingApi UpdateNowPlayingApi = new();
    }
}

namespace LiteFM.Abstractions.ApiContracts
{
    public sealed class UpdateNowPlayingApi : LastFMContractBase<UpdateNowPlayingRequest, UpdateNowPlayingResponse, LastFMJsonError>
    {
        public override HttpRequestMessage MapRequest(LastFMOptions options, string? token, UpdateNowPlayingRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (token == null) throw new ArgumentNullException(nameof(token));
            List<KeyValuePair<string, string>> formData =
            [
                new ("artist", request.Artist),
                new ("track", request.Track),
                new ("album", request.Album),
                new ("api_key", options.ApiKey),
                new ("sk", token),
                new ("method", "track.updateNowPlaying")
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

    public sealed class UpdateNowPlayingRequest : LastFMRequestBase
    {
        public required string Artist;
        public required string Track;
        public required string Album;
        public readonly Uri Endpoint = new("http://ws.audioscrobbler.com/2.0/?format=json");
    }
    public sealed class UpdateNowPlayingResponse : LastFMResponseBase
    {
        [JsonPropertyName("nowplaying")]
        public NowPlayingInfo? NowPlaying { get; set; }
        public class NowPlayingInfo
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
        }
    }
}
