﻿using Newtonsoft.Json;
using System.Net;
using System.Text;
using Ganss.Xss;
using Microsoft.AspNetCore.Http.Features;
namespace ClubWebsite.Middleware
{
    public class AntiXssMiddleware
    {
        private readonly RequestDelegate _next;
        private ErrorResponse _error;
        private readonly int _statusCode = (int)HttpStatusCode.BadRequest;

        public AntiXssMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            // Check XSS in URL
            if (!string.IsNullOrWhiteSpace(context.Request.Path.Value))
            {
                var url = context.Request.Path.Value;

                if (CrossSiteScriptingValidation.IsDangerousString(url, out _))
                {
                    await RespondWithAnError(context).ConfigureAwait(false);
                    return;
                }
            }

            // Check XSS in query string
            if (!string.IsNullOrWhiteSpace(context.Request.QueryString.Value))
            {
                var queryString = WebUtility.UrlDecode(context.Request.QueryString.Value);

                if (CrossSiteScriptingValidation.IsDangerousString(queryString, out _))
                {
                    await RespondWithAnError(context).ConfigureAwait(false);
                    return;
                }
            }

            // Check XSS in request content
            var originalBody = context.Request.Body;
            //var content = await ReadRequestBody(context);
            try
            {
                if(!(context.Request.Method == "POST" && context.Request.ContentType != null && context.Request.ContentType.ToLower().Contains("multipart/form-data")))
                {
                    var content = await ReadRequestBody(context);
                    content = content == null ? "" : content.Trim();
                    var sanitizer = new HtmlSanitizer();

                    var sanitized = sanitizer.Sanitize(content);
                    sanitized = sanitized
                                    .Replace("\r", "")
                                    .Replace("&amp;", "&")
                                    .Replace("&quot;", "\"")
                                    .Replace("&lt;", "<")
                                    .Replace("&rsquo;", "'")
                                    .Replace("&apos;", "'")
                                    .Replace("&gt;", ">")
                                    .Replace("&#", "")
                                    .Replace("&nbsp;", " ")
                                    .Replace("&not", "¬")
                                    .Replace("&reg", "®");
                    content = content
                                    .Replace("\r", "")
                                    .Replace(" />", ">")
                                    .Replace("&amp;", "&")
                                    .Replace("&quot;", "\"")
                                    .Replace("&lt;", "<")
                                    .Replace("&gt;", ">")
                                    .Replace("&rsquo;", "'")
                                    .Replace("&apos;", "'")
                                    .Replace("&#", "")
                                    .Replace("&nbsp;", " ")
                                    .Replace("&not", "¬")
                                    .Replace("&reg", "®");

                    if (sanitized.Length != content.Length)
                    {
                        await RespondWithAnError(context).ConfigureAwait(false);
                        return;
                    }
                }
                await _next(context).ConfigureAwait(false);
            }
            finally
            {
                if (context.Response.StatusCode>=400)
                {
                    context.Request.Path = "/Join/Error";
                    context.Response.StatusCode = 200;
                    await _next(context).ConfigureAwait(false);
                }
                context.Request.Body = originalBody;
            }
        }

        private static async Task<string> ReadRequestBody(HttpContext context)
        {
            var buffer = new MemoryStream();
            await context.Request.Body.CopyToAsync(buffer);
            context.Request.Body = buffer;
            buffer.Position = 0;

            var encoding = Encoding.UTF8;

            var requestContent = await new StreamReader(buffer, encoding).ReadToEndAsync();
            context.Request.Body.Position = 0;

            return requestContent;
        }

        private async Task RespondWithAnError(HttpContext context)
        {
            context.Response.Clear();
            context.Response.Headers.AddHeaders();
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.StatusCode = _statusCode;

            if (_error == null)
            {
                _error = new ErrorResponse
                {
                    Description = "Error from AntiXssMiddleware",
                    ErrorCode = 500
                };
            }

            await context.Response.WriteAsync(_error.ToJSON());
        }
    }

    public static class AntiXssMiddlewareExtension
    {
        public static IApplicationBuilder UseAntiXssMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AntiXssMiddleware>();
        }
    }


    /// <summary>
    /// Imported from System.Web.CrossSiteScriptingValidation Class
    /// </summary>
    public static class CrossSiteScriptingValidation
    {
        private static readonly char[] StartingChars = { '<', '&' };

        #region Public methods

        public static bool IsDangerousString(string s, out int matchIndex)
        {
            //bool inComment = false;
            matchIndex = 0;

            for (var i = 0; ;)
            {

                // Look for the start of one of our patterns 
                var n = s.IndexOfAny(StartingChars, i);

                // If not found, the string is safe
                if (n < 0) return false;

                // If it's the last char, it's safe 
                if (n == s.Length - 1) return false;

                matchIndex = n;

                switch (s[n])
                {
                    case '<':
                        // If the < is followed by a letter or '!', it's unsafe (looks like a tag or HTML comment)
                        if (IsAtoZ(s[n + 1]) || s[n + 1] == '!' || s[n + 1] == '/' || s[n + 1] == '?') return true;
                        break;
                    case '&':
                        // If the & is followed by a #, it's unsafe (e.g. S) 
                        if (s[n + 1] == '#') return true;
                        break;

                }

                // Continue searching
                i = n + 1;
            }
        }

        #endregion

        #region Private methods

        private static bool IsAtoZ(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        #endregion

        public static void AddHeaders(this IHeaderDictionary headers)
        {
            if (headers["P3P"].IsNullOrEmpty())
            {
                headers.Add("P3P", "CP=\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\"");
            }
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }
        public static string ToJSON(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }

    public class ErrorResponse
    {
        public int ErrorCode { get; set; }
        public string Description { get; set; }
    }
}
