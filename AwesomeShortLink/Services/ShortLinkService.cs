using AwesomeShortLink.Exceptions;
using AwesomeShortLink.Models;
using AwesomeShortLink.Repository;
using AwesomeShortLink.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeShortLink.Services
{
    public class ShortLinkService
    {
        public HttpContext Context { get; set; }
        private readonly CommonValidator Validator;
        private readonly DefaultHttpException ExceptionThrower;
        private readonly LinkRepository LinkRepository;
        public ShortLinkService(HttpContext Context)
        {
            this.Context = Context;
            Validator = new CommonValidator() { Context = Context };
            ExceptionThrower = new DefaultHttpException() { Context = Context };
            LinkRepository = new LinkRepository(Context);
        }

        public Dictionary<string, string> EncodeUrl()
        {
            try
            {
                string url = Validator.ValidateEmptyQueryParam("url");
                Validator.ValidateUri(url);

                var entry = new ShortLink
                {
                    Url = url,
                };

                LinkRepository.Insert(entry);

                return entry.GetShortUrlJson(Context.Request.Scheme, Context.Request.Host);
            }
            catch (Exception e)
            {
                return ExceptionThrower.ThrowSimpleException(e.Message);
            }
        }

        private ShortLink DecodeRequestUrl(string code)
        {
            try
            {
                var id = ShortLink.GetId(code);

                var entry = LinkRepository.FindOneById(id);

                return entry;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task RedirectToUrl(string code)
        {
            var entry = DecodeRequestUrl(code);

            if (entry != null)
                Context.Response.Redirect(entry.Url);

            return Task.CompletedTask;
        }

    }
}
