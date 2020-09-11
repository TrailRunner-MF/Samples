using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace SocialLoginWebAPITest
{
    public partial class PostRedirector : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String linkUrl =
                    (String)(this.Session["PostRedirectUrl"]);
                String postParams =
                    (String)(this.Session["PostRedirecParameters"]);

                if (!String.IsNullOrEmpty(linkUrl))
                {
                    this.Session.Remove("PostRedirectUrl");
                    this.Session.Remove("PostRedirecParameters");

                    ExecPostInstruction(linkUrl, postParams);
                }
            }
        }

        private void ExecPostInstruction(String linkUrl, String postParams)
        {
            string formTag = string.Empty;
            string StopOPR = ConfigurationManager.AppSettings["StopOnPostRedirector"] ?? string.Empty;
            if (StopOPR.Equals("true", StringComparison.CurrentCultureIgnoreCase))
            {
                formTag =
@"<form name='form_main' method='post' action='{0}'>
<p>action = {0} </p>
{1}
    <input type='submit' value='Submit'>
</form>";
                postParams = postParams.Replace("hidden", "text");
            }
            else
            {
                formTag =
@"<form name='form_main' method='post' action='{0}'>
{1}
<script language='JavaScript'>document.form_main.submit();</script>
</form>";
            }
            formTag = String.Format(formTag, linkUrl, postParams);

            this.postParameter.InnerHtml = formTag;
            this.postParameter.DataBind();
        }

    }

}