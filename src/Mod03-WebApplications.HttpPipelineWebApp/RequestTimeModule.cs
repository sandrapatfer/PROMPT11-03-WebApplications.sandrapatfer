using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mod03_WebApplications.ThumbsAndWatermarking.WebApp
{
    public class RequestTimeModule : IHttpModule
    {
        #region IHttpModule Members

        public void Dispose()
        {
        }

        public void Init(HttpApplication app)
        {
            app.BeginRequest += new EventHandler(app_BeginRequest);
            app.PostRequestHandlerExecute += new EventHandler(app_PostRequestHandlerExecute);
        }

        void app_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Items.Add("startTime", DateTime.Now);
        }

        void app_PostRequestHandlerExecute(object sender, EventArgs e)
        {
            DateTime startTime = (DateTime)HttpContext.Current.Items["startTime"];
            if (HttpContext.Current.Session != null)
            {
                (HttpContext.Current.Session["requests"] as List<RequestInfo>).Add(new RequestInfo() { Duration = DateTime.Now - startTime, Url = HttpContext.Current.Request.Url.ToString() });
            }
        }


        #endregion
    }
}