using IdentityServer4;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IdentityServerWebAPI.Controllers
{

    [ApiController]
    [Route("identity/[controller]")]
    [SecurityHeaders]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        /// <summary>
        /// This sample controller implements a typical login/logout/provision workflow for local and external accounts.
        /// The login service encapsulates the interactions with the user data store. This data store is in-memory only and cannot be used for production!
        /// The interaction service provides a way for the UI to communicate with identityserver for validation and context retrieval
        /// </summary>
        private readonly TestUserStore _users;

        public LoginController(TestUserStore users = null)
        {
            _users = users ?? new TestUserStore(TestUsers.Users);
        }

        /// <summary>
        /// Send login request to the login web api.
        /// </summary>
        /// <param name="un">User Name</param>
        /// <param name="ru">ReturnUrl</param>
        /// <param name="rm">RememberMe</param>
        /// <returns></returns>
        [HttpGet]
        public async Task GetLogin(string un, string ru, bool rm)
        {
            TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);
            var user = _users.FindByUsername(un);

            AuthenticationProperties props = null;
            if (rm)
            {
                props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(RememberMeLoginDuration)
                };
            };

            // issue authentication cookie with subject ID and username
            var isuser = new IdentityServerUser(user.SubjectId)
            {
                DisplayName = user.Username
            };

            await HttpContext.SignInAsync(isuser, props);

            if (string.IsNullOrEmpty(ru))
            {
                ru = "https://localhost:44390/";
            }
            HttpContext.Response.Redirect(ru);
        }


    }
}
