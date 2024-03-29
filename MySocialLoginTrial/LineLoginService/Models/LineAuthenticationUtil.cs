﻿using LineLoginService.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace LineLoginService.Models
{
    public class LineAuthenticationUtil
    {
        private const string USER_ID = "LINEAccountUserID";
        private const string RETURN_URL = "LINEAccount_ReturnUrl";

        private HttpContext context;
        private ClaimsPrincipal user;

        public LineAuthenticationUtil(HttpContext argcontext, ClaimsPrincipal arguser)
        {
            this.context = argcontext;
            user = arguser;
        }

        public async Task<LineAuthenticationInfo> GetLineAuthInfoAsync(string providerID, string userID, string returnUrl)
        {
            var accessToken = await context.GetTokenAsync("access_token") ?? string.Empty;
            var providerKey = string.Empty;
            var email = string.Empty;
            if (user.Identity.IsAuthenticated)
            {
                providerKey = user.Claims.ToList<System.Security.Claims.Claim>()[0].Value;
                if (user.Identities.First() != null)
                {
                    var claim_email = user.Identities.First().Claims.Where(c => c.Type.Contains("/identity/claims/emailaddress")).FirstOrDefault();
                    if (claim_email != null)
                    {
                        email = claim_email.Value;
                    }
                }
            }

            var thismodel = new LineAuthenticationInfo
            {
                IsAuthenticated = user.Identity.IsAuthenticated,
                AccessToken = accessToken,
                ProviderID = "LINE",
                ProviderKey = providerKey,
                UserID = userID,
                Email = email,
                ReturnUrl = returnUrl,
            };
            return thismodel;
        }

    }

}
