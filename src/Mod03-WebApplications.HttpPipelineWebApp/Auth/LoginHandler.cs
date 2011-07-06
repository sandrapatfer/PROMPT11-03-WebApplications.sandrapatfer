using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mod03_WebApplications.ThumbsAndWatermarking.WebApp.Auth
{
    public class LoginHandler : IHttpHandler
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var user = context.Request.Form["UserName"];
            var pwd = context.Request.Form["Password"];

            // do something to authenticate user

            MyAuthentication.AuthenticateUser(user, null);

            HttpContext.Current.Response.Redirect(MyAuthentication.RedirectUrl);
        }

        #endregion
    }
}