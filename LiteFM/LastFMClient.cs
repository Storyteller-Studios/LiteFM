using LiteFM.Abstractions;
using LiteFM.Abstractions.ApiContracts;
using LiteFM.Abstractions.Bases;
using LiteFM.Extensions.JsonConverters;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LiteFM
{
    public class LastFMClient
    {
        private readonly HttpClient _httpClient;
        public LastFMOptions Options { get; }
        public JsonSerializerOptions DefaultOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            TypeInfoResolver = LastFMJsonDefaultContext.Default,
            Converters = { new JsonBooleanConverter() }
        };
        public LastFMClient(LastFMOptions options)
        {
            var handler = new HttpClientHandler()
            {
                UseProxy = options.UseProxy,
                UseCookies = false
            };
            _httpClient = new HttpClient(handler);
            Options = options;
        }
        public LastFMClient(LastFMOptions options, HttpClient httpClient)
        {
            _httpClient = httpClient;
            Options = options;
        }
        public async Task<Results<TResponse, TError>> RequestAsync<TRequest, TResponse, TError>(LastFMContractBase<TRequest, TResponse, TError> contrast, TRequest request, string? token = null) 
            where TRequest : LastFMRequestBase
            where TResponse : LastFMResponseBase
            where TError : LastFMErrorBase
        {
            using var message = contrast.MapRequest(Options, token, request);
            using var responseMessage = await _httpClient.SendAsync(message);
            var stringContent = await responseMessage.Content.ReadAsStringAsync();
            var result = new Results<TResponse, TError>();
#if DEBUG

            result.OriginalResponse = stringContent;
#endif
            result.IsSuccess = responseMessage.IsSuccessStatusCode;
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = JsonSerializer.Deserialize<TResponse>(stringContent, DefaultOptions);
                result.Response = content;
            }
            else
            {
                var content = JsonSerializer.Deserialize<TError>(stringContent, DefaultOptions);
                result.Error = content;
            }
            return result;
        }
        public Task<Results<TResponse, TError>> RequestAsync<TRequest, TResponse, TError>(LastFMContractBase<TRequest, TResponse, TError> contrast, TRequest request, LastFMSession session)
            where TRequest : LastFMRequestBase
            where TResponse : LastFMResponseBase
            where TError : LastFMErrorBase
        {
            return RequestAsync(contrast, request, session.Key);
        }
    }

    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(GetSessionResponse))]
    [JsonSerializable(typeof(UpdateNowPlayingResponse))]
    [JsonSerializable(typeof(ScrobbleResponse))]
    [JsonSerializable(typeof(LastFMJsonError))]
    [JsonSerializable(typeof(CorrectedItem))]
    [JsonSerializable(typeof(IgnoredMessage))]
    public partial class LastFMJsonDefaultContext : JsonSerializerContext
    {

    }
}
