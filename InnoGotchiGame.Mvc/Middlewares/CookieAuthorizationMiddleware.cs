using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InnoGotchiGame.Mvc.Middlewares
{
    public class CookieAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public CookieAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var jwtToken = context.Request.Cookies["jwtToken"];
            if (jwtToken is not null)
                context.Request.Headers.Add("Authorization", "Bearer " + jwtToken);
            await _next.Invoke(context);
        }
    }
}
