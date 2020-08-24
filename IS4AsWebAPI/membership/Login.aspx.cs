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
            string reutnrUrl = "https://localhost:44318/home/";
            string target = "https://localhost:5001/Account/Login2?un={0}&pw={1}&rl={2}&ru={3}";
            target = string.Format(target, this.txtLoginID.Text, this.txtPassword.Text,
                this.chkRememberme.Checked.ToString(), reutnrUrl);

            Response.Redirect(target);
        }

    }
}