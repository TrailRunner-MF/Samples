using LineLoginService.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;


namespace LineLoginService.Controllers
{
    public class AccountController : Controller
    {
        private const string PROVIDER_ID = "LINE";
        private const string USER_ID = "LINEAccountUserID";
        private const string RETURN_URL = "LINEAccount_ReturnUrl";
        private const string LINE_AUTH_INFO = "LINEAccount_LineAuthInfo";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginByLINE(string userID, string returnUrl)
        {
            var redirectUrl = "/Account/LoginCompleted";
            HttpContext.Session.SetString(USER_ID, userID ?? string.Empty);
            HttpContext.Session.SetString(RETURN_URL, returnUrl ?? string.Empty);
            return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl }, OpenIdConnectDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> LoginCompleted()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userID = HttpContext.Session.GetString(USER_ID) ?? string.Empty;
                var returnUrl = HttpContext.Session.GetString(RETURN_URL) ?? string.Empty;
                HttpContext.Session.Remove(USER_ID);
                HttpContext.Session.Remove(RETURN_URL);
                var myutil = new LineAuthenticationUtil(HttpContext, User);
                var authInfo = await myutil.GetLineAuthInfoAsync(PROVIDER_ID, userID, returnUrl);
                string lineauthinfo = Newtonsoft.Json.JsonConvert.SerializeObject(authInfo);

                HttpContext.Session.SetString(LINE_AUTH_INFO, lineauthinfo);
            }

            var redirectUrl = "/";
            return Redirect(redirectUrl);
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove(LINE_AUTH_INFO);
            var redirectUrl = "/";
            return SignOut(new AuthenticationProperties { RedirectUri = redirectUrl }, CookieAuthenticationDefaults.AuthenticationScheme);
        }

    }
}
