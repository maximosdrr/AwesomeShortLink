using AwesomeShortLink.Exceptions;
using AwesomeShortLink.Models;
using AwesomeShortLink.Services;
using LiteDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeShortLink.Controllers
{
    [Route("")]
    [ApiController]
    public class ShortLinkController : ControllerBase
    {
        [HttpGet("{code}")]
        public void Redirect(string code)
        {
            ShortLinkService service = new(HttpContext);

            service.RedirectToUrl(code);
        }

        [HttpGet("/short-link")]
        public Dictionary<string, string> ShortLink()
        {
            ShortLinkService service = new(HttpContext);

            return service.EncodeUrl();
        }




    }
}
