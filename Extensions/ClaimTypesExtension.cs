﻿using System.Security.Claims;
using System.Xml.Linq;

namespace JwtAspNet.Extensions
{
    public static class ClaimTypesExtension
    {
        public static int GetId(this ClaimsPrincipal user)
        {
            try
            {
                var id = user.Claims.FirstOrDefault(x => x.Type == "id")?.Value ?? "0";
                return int.Parse(id);
            }
            catch
            {
                return 0;
            }
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            try
            {
                return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetName(this ClaimsPrincipal user)
        {
            try
            {
                return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetGivenName(this ClaimsPrincipal user)
        {
            try
            {
                return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }        
    }
}
