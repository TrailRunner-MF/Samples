using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace MySocialLoginService.Pages.SocialLogin
{

    [IgnoreAntiforgeryToken]
    public class MyExternalLoginsModel : PageModel
    {
        private const string USE_ID = "MyExternalLogin_UserID";
        private const string RETURN_URL = "MyExternalLogin_ReturnUrl";

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public MyExternalLoginsModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IList<UserLoginInfo> CurrentLogins { get; set; }

        public IList<AuthenticationScheme> OtherLogins { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            OtherLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync())
                .ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostLinkLoginAsync(string provider, string userId = null, string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Put argument userId and returnUrl into Session.
            if (string.IsNullOrEmpty(userId))
            {
                userId = "(not set)";
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = WebUtility.UrlDecode(returnUrl);
            }
            else
            {
                returnUrl = "(not set)";
            }
            HttpContext.Session.SetString(USE_ID, userId);
            HttpContext.Session.SetString(RETURN_URL,returnUrl);

            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Page("/SocialLogin/MyExternalLogins", pageHandler: "LinkLoginCallback");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, userId);

            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetLinkLoginCallbackAsync()
        {
            // pull urseId and returnUrl from Session and remove them.
            var userId = HttpContext.Session.GetString(USE_ID);
            var returnUrl = HttpContext.Session.GetString(RETURN_URL);
            HttpContext.Session.Remove(USE_ID);
            HttpContext.Session.Remove(RETURN_URL);

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                throw new InvalidOperationException($"Unexpected error occurred loading external login info.");
            }
            var message = string.Format("Social Account for ProviderID: {0} and ProviderKey: {1} will bind to " + 
                "Sight memeber with UserId: {2}, then redirect to {3}. " +
                "(This part is not implemented in this program.)",
                info.LoginProvider, info.ProviderKey, userId, returnUrl); ;
            StatusMessage = message;

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return RedirectToPage();
        }
    }
}
