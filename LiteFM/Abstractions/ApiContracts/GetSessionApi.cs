using LiteFM.Abstractions.ApiContracts;
using LiteFM.Abstractions.Bases;
using System;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace LiteFM.Api
{
    public static partial class LastFMApi
    {
        public static readonly GetSessionApi GetSessionApi = new();
    }
}

namespace LiteFM.Abstractions.ApiContracts
{
    public sealed class GetSessionApi : LastFMContractBase<GetSessionRequest, GetSessionResponse, LastFMJsonError>
    {
        public override HttpRequestMessage MapRequest(LastFMOptions options, string? token, GetSessionRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var sig = Utils.GetLastFMAPISignature(options.ApiSecret,
                [new("api_key", options.ApiKey), new("method", "auth.getSession"), new("token", request.Token)]);
            var result = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = 
                    new Uri($"https://ws.audioscrobbler.com/2.0/?method=auth.getSession&format=json&token={request.Token}&api_key={options.ApiKey}&api_sig={sig}")
            };
            return result;
        }
    }

    public sealed class GetSessionRequest : LastFMRequestBase
    {
        public required string Token;
    }
    public sealed class GetSessionResponse : LastFMResponseBase
    {
        [JsonPropertyName("session")]
        public LastFMSession? Session { get; set; }
    }
}
