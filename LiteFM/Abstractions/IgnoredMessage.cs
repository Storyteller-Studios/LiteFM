using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LiteFM.Abstractions
{
    public class IgnoredMessage
    {
        [JsonPropertyName("code")]

        public bool? Code { get; set; }
        [JsonPropertyName("#text")]
        public string? Text { get; set; }
    }
}
