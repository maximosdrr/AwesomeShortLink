using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeShortLink.Exceptions
{
    public class DefaultHttpException
    {
        public HttpContext Context { get; set; }

        public Dictionary<string, string> ThrowSimpleException(string errorName, int code = StatusCodes.Status400BadRequest, string errorKey = "error")
        {
            Context.Response.StatusCode = code;

            var error = new Dictionary<string, string>()
            {
                [errorKey] = errorName,
            };

            return error;
        }
    }
}
