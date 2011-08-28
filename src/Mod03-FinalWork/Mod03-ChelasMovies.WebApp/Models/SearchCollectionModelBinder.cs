using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mod03_ChelasMovies.WebApp.Models
{
    using DomainModel;

    public class SearchCollectionModelBinder : IModelBinder
    {
        #region IModelBinder Members

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            SearchCollection searchList = new SearchCollection();
            foreach (var s in controllerContext.HttpContext.Request.QueryString.AllKeys)
            {
                if (s.StartsWith("search_"))
                {
                    searchList.Add(s.Substring(7), controllerContext.HttpContext.Request.QueryString[s]);
                }
            }

            return searchList;
        }

        #endregion
    }
}