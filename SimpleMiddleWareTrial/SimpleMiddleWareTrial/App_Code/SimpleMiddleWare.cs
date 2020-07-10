using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace SimpleMiddleWareTrial.App_Code
{
    public class SimpleMiddleWare
    {
        private readonly RequestDelegate _next;

        public SimpleMiddleWare(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var musicData = new string[] {
                new MusicData("Better now", "Hip-Hop", "Post Malone").ToString(),
                new MusicData("I fell energy", "Alternative-Rock", "Dirty Projecters").ToString(),
                new MusicData("Rite of Spring", "Claasical", "Igor Stravinsky").ToString(),
            };
            var sessionValue = string.Empty;
            foreach (var item in musicData)
            {
                sessionValue += item + "|";
            }
            context.Session.SetString("MusicData", sessionValue);
            await _next(context);
        }
    }

    public static class SimpleMiddleWareExtensions
    {
        public static IApplicationBuilder UseSimpleMiddleWare(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SimpleMiddleWare>();
        }
    }

}
