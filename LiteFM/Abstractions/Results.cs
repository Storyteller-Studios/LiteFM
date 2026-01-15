using LiteFM.Abstractions.Bases;

namespace LiteFM.Abstractions
{
    public class Results<TResponse, TError>
        where TResponse : LastFMResponseBase
        where TError : LastFMErrorBase
    {
        public bool IsSuccess { get; internal set; }
        public TResponse? Response { get; internal set; }
        public TError? Error { get; internal set; }
#if DEBUG
        public string? OriginalResponse { get; internal set; }
#endif
    }
}
