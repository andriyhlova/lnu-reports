using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace SRS.Web.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString YesNo(this HtmlHelper htmlHelper, bool value)
        {
            var text = value ? "Так" : "Ні";
            return new MvcHtmlString(text);
        }
    }
}