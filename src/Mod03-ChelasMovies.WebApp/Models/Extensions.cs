using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mod03_ChelasMovies.WebApp
{
    public static class Extensions
    {
        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string imageUrl)
        {
            return MvcHtmlString.Create(string.Format("<img src=\"{0}\" alt=\"{0}\" />", imageUrl));
        }
    }
}