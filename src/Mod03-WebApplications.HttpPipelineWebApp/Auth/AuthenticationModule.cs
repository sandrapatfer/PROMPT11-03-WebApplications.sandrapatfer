using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace Mod03_WebApplications.ThumbsAndWatermarking.WebApp.Auth
{
    public class AuthenticationModule : IHttpModule
    {
        #region IHttpModule Members

        public void Dispose()
        {
        }

        public void Init(HttpApplication app)
        {
            app.AuthenticateRequest += new EventHandler(app_AuthenticateRequest);
            app.EndRequest += new EventHandler(app_EndRequest);
        }

        #endregion

        void app_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpContext.Current.User = MyAuthentication.GetCurrentUser();
        }

        void app_EndRequest(object sender, EventArgs e)
        {
            // error in authorization, redirect to login page
            if (HttpContext.Current.Response.StatusCode == 401)
            {
                HttpContext.Current.Response.Redirect(MyAuthentication.RedirectLoginUrl);
            }
        }

    }
}