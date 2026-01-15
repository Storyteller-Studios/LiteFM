using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LiteFM.Abstractions
{
    internal static class Utils
    {
        internal static MD5 MD5 = MD5.Create();
        internal static string GetLastFMAPISignature(string key, string secret, string method)
        {

            var byteData = Encoding.UTF8.GetBytes($"api_key{key}method{method}{secret}");
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
