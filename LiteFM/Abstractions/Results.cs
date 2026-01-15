using LiteFM.Abstractions.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
