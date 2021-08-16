using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeShortLink.Models
{
    public class ShortLink
    {
        public int Id { get; set; }
        public string Url { get; set; }

        private string GetUrlChunk()
        {
            return WebEncoders.Base64UrlEncode(BitConverter.GetBytes(Id));
        }

        public static int GetId(string urlChunck)
        {
            return BitConverter.ToInt32(WebEncoders.Base64UrlDecode(urlChunck));
        }

       

        public Dictionary<string, string> GetShortUrlJson(string Scheme, HostString Host)
        {
            var response = new Dictionary<string, string>();
            var urlChunk = this.GetUrlChunk();

            response.Add("url", $"{Scheme}://{Host}/{urlChunk}");
            response.Add("original_url", Url);

            return response;
        }
    }
}
