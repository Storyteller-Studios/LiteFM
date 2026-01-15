using System.Net.Http;

namespace LiteFM.Abstractions.Bases
{
    public abstract class LastFMContractBase<TRequest, TResponse, TError>
        where TRequest : LastFMRequestBase
        where TResponse : LastFMResponseBase
        where TError : LastFMErrorBase
    {
        public abstract HttpRequestMessage MapRequest(LastFMOptions options, string? token, TRequest request);
    }
}
