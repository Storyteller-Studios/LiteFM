using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteFM.Abstractions
{
    public class LastFMOptions
    {
        public required string ApiKey { get; init; }
        public required string ApiSecret { get; init; }
        public bool UseProxy { get; init; } = false;
    }
}
