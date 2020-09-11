using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace membership
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string reutnrUrl = "https://localhost:44390/";
            string target = "https://localhost:5001/identity/login?un={0}&ru={1}&rm={2}";
            target = string.Format(target, this.txtLoginID.Text,
                Server.UrlEncode(reutnrUrl),
                this.chkRememberme.Checked.ToString());

            Response.Redirect(target);
        }

    }
}