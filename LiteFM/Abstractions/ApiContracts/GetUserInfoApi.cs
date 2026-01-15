using LiteFM.Abstractions.ApiContracts;
using LiteFM.Abstractions.Bases;
using System;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace LiteFM.Api
{
    public static partial class LastFMApi
    {
        public static readonly GetUserInfoApi GetUserInfoApi = new();
    }
}

namespace LiteFM.Abstractions.ApiContracts
{
    public sealed class GetUserInfoApi : LastFMContractBase<GetUserInfoRequest, GetUserInfoResponse, LastFMJsonError>
    {
        public override HttpRequestMessage MapRequest(LastFMOptions options, string? token, GetUserInfoRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var result = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri =
                    new Uri($"https://ws.audioscrobbler.com/2.0/?method=user.getinfo&user={request.User}&api_key={options.ApiKey}&format=json")
            };
            return result;
        }
    }

    public sealed class GetUserInfoRequest : LastFMRequestBase
    {
        public required string User;
    }
    public sealed class GetUserInfoResponse : LastFMResponseBase
    {
        [JsonPropertyName("user")]
        public LastFMUser? User { get; set; }
    }
}
