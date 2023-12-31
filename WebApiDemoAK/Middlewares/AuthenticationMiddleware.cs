﻿using System.Security.Claims;
using System.Text;
using WebApiDemoAK.Services.Asbtract;

namespace WebApiDemoAK.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            if (authHeader != null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring(6).Trim();
                string credentialString = "";
                try
                {
                    credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    return;
                }

                var credentials = credentialString.Split(':');
                var username = credentials[0];
                var password = credentials[1];

                var _studentService = context.RequestServices.GetRequiredService<IStudentService>();

                var student = _studentService.GetAll()
                    .FirstOrDefault(s => s.Username == username
                     && s.Password == password);
                if (student!=null)//for testing
                {
                    var claim = new[]
                    {
                        new Claim("username",username),
                        new Claim(ClaimTypes.Role,"Admin")
                    };

                    var identity = new ClaimsIdentity(claim, "Basic");
                    context.User = new ClaimsPrincipal(identity);
                    await _next(context);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }
        }
    }
}
