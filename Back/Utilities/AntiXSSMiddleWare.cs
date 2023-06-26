using Ganss.Xss;
using System.Text;

namespace Back.Utilities
{
    public class AntiXSSMiddleWare
    {
        private readonly RequestDelegate _next;

        public AntiXSSMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();
            using (StreamReader sr = new(context.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                string? raw = await sr.ReadToEndAsync();
                HtmlSanitizer sanitizer = new();
                string sanitized = sanitizer.Sanitize(raw);

                if (raw != sanitized)
                {
                    throw new BadHttpRequestException("XSS injection detected.");
                }
            }

            context.Request.Body.Seek(0, SeekOrigin.Begin);
            await _next.Invoke(context);
        }
    }
}