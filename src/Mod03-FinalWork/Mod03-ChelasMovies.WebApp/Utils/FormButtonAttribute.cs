using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;

namespace Mod03_ChelasMovies.WebApp.Utils
{
    public class FormButtonAttribute : ActionMethodSelectorAttribute
    {
        private string _buttonName;
        private string _buttonValue;

        public FormButtonAttribute(string buttonName, string buttonValue)
        {
            _buttonName = buttonName;
            _buttonValue = buttonValue;
        }

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return _buttonValue.Equals(controllerContext.HttpContext.Request.Form[_buttonName]);
        }
    }
}