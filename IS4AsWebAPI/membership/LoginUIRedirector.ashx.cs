using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace membership
{
    /// <summary>
    /// LoginUIRedirector の概要の説明です
    /// </summary>
    public class LoginUIRedirector : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Write("Redirected by LoginUIRedirector.ashx");
            context.Response.Redirect("Login.aspx?kicker=loginuiredirector", false);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}