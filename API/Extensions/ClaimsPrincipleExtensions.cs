using System.Security.Claims;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string Getemail(this ClaimsPrincipal user) /// geting an username by user  :depends on token 
        {
            return user.Claims.FirstOrDefault(s => s.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        public static int GetUserId(this ClaimsPrincipal user)/// geting an id by user  :depends on token 
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}