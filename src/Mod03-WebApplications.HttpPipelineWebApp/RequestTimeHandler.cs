using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Text;

namespace Mod03_WebApplications.ThumbsAndWatermarking.WebApp
{
    public class RequestTimeHandler : IHttpHandler, IRequiresSessionState
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            var template = new StreamReader(@"C:\SPF\prompt11\uc03\PROMPT11-03-WebApplications.sandrapatfer\src\Mod03-WebApplications.HttpPipelineWebApp\RequestTimeTemplate.htm").ReadToEnd();
            var requests = context.Session["requests"] as List<RequestInfo>;
            var str = new StringBuilder();
            requests.ForEach(ri =>str.Append(String.Format("<div>Url: {0} Request Time: {1}</div>\n", ri.Url, ri.Duration)));
            context.Response.Write(string.Format(template, str.ToString()));
        }

        #endregion
    }
}