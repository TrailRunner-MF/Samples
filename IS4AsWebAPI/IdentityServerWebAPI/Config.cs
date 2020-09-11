using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServerWebAPI
{
    public static class Config
    {

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("apiIS4WebAPI", "API for IdentityServer4 As WebAPI")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
             
                // interactive ASP.NET Core Razor Page client
                new Client
                {
                    ClientId = "frontpage",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    
                    // where to redirect to after login
                    RedirectUris = { "https://localhost:44390/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:44390/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "apiIS4WebAPI"
                    }
                }
            };


    }

}
