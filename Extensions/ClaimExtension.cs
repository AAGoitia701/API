﻿using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace WebAPI.Extensions
{
    public static class ClaimExtension
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;
        }
    }
}
