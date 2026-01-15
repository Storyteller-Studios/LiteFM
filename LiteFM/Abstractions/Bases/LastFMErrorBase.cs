using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LiteFM.Abstractions.Bases
{
    public class LastFMErrorBase : LastFMResponseBase
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("error")]
        public int Error { get; set; } = 0;
    }
}
