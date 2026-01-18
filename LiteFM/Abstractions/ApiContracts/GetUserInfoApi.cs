using LiteFM.Abstractions.ApiContracts;
using LiteFM.Abstractions.Bases;
using System;
using System.Net.Http;
using System.Text;
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
            if (request == null && string.IsNullOrEmpty(token)) throw new ArgumentNullException(nameof(request));
            var result = new HttpRequestMessage()
            {
                Method = HttpMethod.Get
            };
            var sb = new StringBuilder($"https://ws.audioscrobbler.com/2.0/?method=user.getinfo&api_key={options.ApiKey}&format=json");
            if (request != null)
            {
                sb.Append($"&user={request.User}");
            }
            if (token != null)
            {
                sb.Append($"&sk={token}");
            }
            result.RequestUri = new Uri(sb.ToString());
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
