using System.Security.Cryptography;
using System.Text;

namespace UrlShortener.Api.Services
{
    public static class UrlHashHelper
    {
        public static string Compute(string originalUrl)
        {
            var bytes = Encoding.UTF8.GetBytes(originalUrl);
            var hash = SHA256.HashData(bytes);
            return Convert.ToHexString(hash);
        }
    }
}
