
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using EmotionSoft.ewd2.SimpleUT;

namespace SocialLoginWebAPITest.App_Code
{

    [FreeTestClass("tc01.Test for Social Login Web Service")]
    public class tc01_SLWebAPITest: WebFreeTestFixture
    {

        #region client
        // HttpClientの特性により静的メンバーにすること。
        // ※）そうしないとシステムリソースを食い尽くす可能性のあるトンデモプログラムらしい！！
        static HttpClient client;
        #endregion

        #region tm11_CallExternalLogins
        /// <summary>
        /// Request getting External-authentication information to 'MySocialLoginService'
        /// </summary>
        [FreeTestMethod("test01.Execute ExternalLogins-Order")]
        public void tm11_CallExternalLogins(string provider, string userID, string returnUrl)
        {
            string actionUrll = "https://localhost:44321/SocialLogin/MyExternalLogins?handler=LinkLogin";

            var dic = new Dictionary<string, string>();
            dic.Add("provider", provider);
            dic.Add("userID", userID);
            dic.Add("returnUrl", Server.UrlEncode(returnUrl));
            dic.Add("__RequestVerificationToke", new Random().NextDouble().ToString());     // This must create correctly!!
            ExecPost(actionUrll, dic);
        }
        #endregion

        #region tm12_CallOtherExternalLogins
        /// <summary>
        /// Request getting External-authentication information to 'OtherSocialLoginService'
        /// </summary>
        [FreeTestMethod("test02.Execute Other ExternalLogins-Order")]
        public void tm12_CallOtherExternalLogins (string provider, string userID, string returnUrl)
        {
            string actionUrll = "https://localhost:44382/SocialLogin/MyExternalLogins?handler=LinkLogin";

            var dic = new Dictionary<string, string>();
            dic.Add("provider", provider);
            dic.Add("userID", userID);
            dic.Add("returnUrl", Server.UrlEncode(returnUrl));
            dic.Add("__RequestVerificationToke", new Random().NextDouble().ToString());     // This must create correctly!!
            ExecPost(actionUrll, dic);
        }
        #endregion

        #region ExternalLoginBinderPattern
        [FreeTestPattern("tm11_CallExternalLogins;tm12_CallOtherExternalLogins", true, false)]
        public List<string[]> ExternalLoginBinderPattern
        {
            get
            {
                var thisUrl = this.Page.Request.Url.ToString();
                var userID = "UID-0001";
                var result = new List<string[]>();
                result.Add(new string[] { "Google",  userID, thisUrl, "Google account." });
                result.Add(new string[] { "Facebook", userID, thisUrl, "Facebook account" });
                result.Add(new string[] { "Microsoft", userID, thisUrl, "Microsoft account" });
                return result;
            }
        }
        #endregion

        #region GetResponseAsync (private)
        /// <summary>
        /// 引数のアドレスに指定されたWebAPIをコールする。
        /// </summary>
        /// <param name="_address">コールするWebAPIのアドレス</param>
        /// <remarks>
        /// HttpClientを使用してWebAPIをコールする非同期処理。
        /// クライアントが同期メソッドの場合これをどうやって呼ぶかがミソ♪
        /// </remarks>
        /// <returns>WebAPIの戻り値(通常はJSON文字列であることを期待する)</returns>
        private async Task<string> GetResponseAsync(string _address)
        {
            client = new HttpClient();
            client.Timeout = new System.TimeSpan(0, 0, 5);
            using (client)
            {
                HttpResponseMessage response = await client.GetAsync(_address);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }
        #endregion

        #region ExecPost (private)
        private void ExecPost(string actionUrl, Dictionary<string, string> paramDics)
        {
            this.Session["PostRedirectUrl"] = actionUrl;

            string parameters = string.Empty;
            foreach (var item in paramDics)
            {
                parameters += string.Format("<div><label>{0}</label><input type='hidden' name='{0}' value='{1}' />",
                    item.Key, item.Value) + Environment.NewLine;
            }
            this.Session["PostRedirecParameters"] = parameters;
            this.RedirectOnNewWindow("PostRedirector.aspx");
        } 
        #endregion

    }

}