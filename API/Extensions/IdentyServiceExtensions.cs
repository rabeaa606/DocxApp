using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentyServiceExtensions
    {


        public static IServiceCollection AddIdentyServiceExtensions(this IServiceCollection services
        , IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),   // the token key
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };

                 options.Events = new JwtBearerEvents
                 {
                     OnMessageReceived = context =>
                     {
                         var accessToken = context.Request.Query["access_token"];  /// get the accrss token

                          var path = context.HttpContext.Request.Path;

                          //if its not an http requist then check if it an signalR requist
                          if (!string.IsNullOrEmpty(accessToken) &&
                              path.StartsWithSegments("/hubs"))
                         {
                             context.Token = accessToken;
                         }

                         return Task.CompletedTask;
                     }
                 };
             });
            return services;
        }
    }
}