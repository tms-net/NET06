using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using TMS.NET06.Parfume.Manager.MVC.Data.Models;

namespace TMS.NET06.Parfume.Manager.MVC.App_Code
{
    public static class MyHelper
    {
        public static string GenderUrl(this IHtmlHelper html,  Gender gender)
        {
            var context = html.ViewContext.HttpContext;
            
            return "/home/shop?gender=" + gender.ToString();
        }
    }
}
