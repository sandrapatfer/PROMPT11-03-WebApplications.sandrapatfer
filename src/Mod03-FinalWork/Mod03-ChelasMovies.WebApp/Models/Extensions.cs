using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Html;

namespace Mod03_ChelasMovies.WebApp
{
    public static class Extensions
    {
        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string imageUrl, bool thumb)
        {
            if (thumb)
                return MvcHtmlString.Create(string.Format("<img src=\"{0}\" alt=\"{0}\" height=100px />", imageUrl));
            else
                return MvcHtmlString.Create(string.Format("<img src=\"{0}\" alt=\"{0}\" />", imageUrl));
        }

        public static MvcHtmlString EnumDropDownList(this HtmlHelper htmlHelper, string name, Type enumType, int selectedEnumVal)
        {
            IEnumerable<SelectListItem> items = Enum.GetValues(enumType).Cast<object>().Select(n =>
            {
                int v = (int)n;
                return new SelectListItem
            {
                Text = n.ToString(),
                Value = v.ToString(),
                Selected = v.Equals(selectedEnumVal)
            };
            }
               );
            //int val = (int)(object)selectedEnumVal;
            //values.Single(v => v.Equals(selectedEnumVal));
            //string s = "Bad";
            //SelectList list = new SelectList(items, "Value", "Name", s);
            MvcHtmlString str = htmlHelper.DropDownList(name, items);
            return str;
        }

        //public static MvcHtmlString EnumDropDownList(this HtmlHelper htmlHelper, string name, Type enumType, object selectedEnumVal)
        //    where T : struct
        //{
        //    var items = Enum.GetNames(enumType).Zip(Enum.GetValues(typeof(enumType)).Cast<int>(), (n, v) => new { Name = n, Value = v });
        //    SelectList list = new SelectList(items, "Value", "Name", (int)selectedEnumVal));
        //    return htmlHelper.DropDownList(name, list);
        //}

        public static MvcHtmlString HearderLink(this HtmlHelper htmlHelper, string header)
        {
            var routeDict = new RouteValueDictionary(htmlHelper.ViewContext.RouteData.Values);
            routeDict["SortingCriteria"] = header; // TODO constants
            return htmlHelper.RouteLink(header, "Paging", routeDict);
        }
    }
}