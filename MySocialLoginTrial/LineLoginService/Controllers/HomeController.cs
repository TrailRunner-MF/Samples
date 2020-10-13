using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using LineLoginService.Models;
using Newtonsoft.Json;

namespace LineLoginService.Controllers
{
    public class HomeController : Controller
    {
        private const string PROVIDER_ID = "LINE";
        private const string LINE_AUTH_INFO = "LINEAccount_LineAuthInfo";

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            LineAuthenticationInfo lineauthinfo = null;
            string infojson = HttpContext.Session.GetString(LINE_AUTH_INFO);
            if (!string.IsNullOrEmpty(infojson))
            {
                lineauthinfo = JsonConvert.DeserializeObject<LineAuthenticationInfo>(infojson);
            }
            else
            {
                if (User.Identity.IsAuthenticated)
                {
                    var authUtil = new LineAuthenticationUtil(HttpContext, User);
                    lineauthinfo = await authUtil.GetLineAuthInfoAsync(PROVIDER_ID, "", "");
                    string authinfoJson = JsonConvert.SerializeObject(lineauthinfo);
                    HttpContext.Session.SetString(LINE_AUTH_INFO, authinfoJson);
                }
            }

            var thismodel = new HomeIndexViewModel
            {
                ProviderID = "LINE",
            };
            if (lineauthinfo != null)
            {
                thismodel.IsAuthenticated = lineauthinfo.IsAuthenticated;
                thismodel.AccessToken = lineauthinfo.AccessToken;
                thismodel.ProviderKey = lineauthinfo.ProviderKey;
                thismodel.UserID = lineauthinfo.UserID;
                thismodel.Email = lineauthinfo.Email;
                thismodel.ReturnUrl = lineauthinfo.ReturnUrl;
            };
           return View(thismodel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
