using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeShortLink.Validators
{
    public class CommonValidator
    {
        public HttpContext Context { get; set; }
        public string ValidateEmptyQueryParam(string param)
        {
            if (!Context.Request.Query.TryGetValue(param, out StringValues paramValue))
            {
                throw new Exception($"{param} must be provided");
            }

            return paramValue;
        }

        public string ValidateUri(string uri)
        {
            if (!Uri.TryCreate(uri, UriKind.Absolute, out Uri uriValue))
            {
                throw new Exception($"{uri} was not a valid uri");
            }

            return uriValue.ToString();
        }
    }
}
