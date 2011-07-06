using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace Mod03_WebApplications.ThumbsAndWatermarking.WebApp.Auth
{
    public class MyAuthentication
    {
        private static string m_userNameString = "userName";
        private static string m_originString = "origin";

        public static void AuthenticateUser(string userName, string[] roles)
        {
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(m_userNameString, userName));
            HttpContext.Current.Response.Cookies[m_userNameString].Expires = DateTime.Now.AddDays(2);
        }

        public static IPrincipal GetCurrentUser()
        {
            if (HttpContext.Current.Request.Cookies[m_userNameString] != null)
            {
                return new GenericPrincipal(new GenericIdentity(HttpContext.Current.Request.Cookies[m_userNameString].Value), new string[1] { "user" });
            }
            else
            {
                return null;
            }
        }

        public static string RedirectUrl
        {
            get
            {
                if (HttpContext.Current.Request.QueryString[m_originString] != null &&
                    HttpContext.Current.Request.QueryString[m_originString] != @"/")
                {
                    return HttpContext.Current.Request.QueryString[m_originString];
                }
                else
                {
                    //return HttpContext.Current.Request.ApplicationPath + @"private";
                    return "~/private";
                }
            }
        }

        public static string LoginUrl
        {
            get
            {
                return "~/public/Login.html";
            }
        }

        public static string RedirectLoginUrl
        {
            get
            {
                return string.Format("{0}?{1}={2}", LoginUrl, m_originString, HttpContext.Current.Request.Url.AbsolutePath);
            }
        }
    }
}