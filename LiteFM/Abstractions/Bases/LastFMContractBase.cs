using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
