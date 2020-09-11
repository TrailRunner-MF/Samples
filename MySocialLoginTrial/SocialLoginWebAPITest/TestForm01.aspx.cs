using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EmotionSoft.ewd2.SimpleUT;
using EmotionSoft.ewd2.SimpleUT.WebControlSet;
using SocialLoginWebAPITest.App_Code;

namespace SocialLoginWebAPITest
{
    public partial class TestForm01 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void FreeTestPanel1_AddTestClassController(object sender,
            EmotionSoft.ewd2.SimpleUT.WebControlSet.AddTestClassControllerEventArgs e)
        {
            Type target = typeof(tc01_SLWebAPITest);
            e.TestClassControllerList.AddClassController(target, this, this.Context);

        }
    }
}