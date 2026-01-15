using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LiteFM
{
    internal static class Utils
    {
        internal static MD5 MD5 = MD5.Create();
        internal static string GetLastFMAPISignature(string secret, IEnumerable<KeyValuePair<string,string>> parameters)
        {
            var processed = parameters.OrderBy(t => t.Key, StringComparer.Ordinal).Select(t => $"{t.Key}{t.Value}").ToList();
            var paramString = string.Concat(processed);
            var byteData = Encoding.UTF8.GetBytes($"{paramString}{secret}");
            var md5 = MD5.ComputeHash(byteData);
            StringBuilder stringBuilder = new();
            foreach (var data in md5)
            {
                stringBuilder.Append(data.ToString("x2"));
            }
            return stringBuilder.ToString();
        }
    }
}
