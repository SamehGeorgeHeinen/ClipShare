using Microsoft.AspNetCore.Mvc.Rendering;


namespace ClipShare.Utility
{
    public static class SD // Changed to static class to fix CS1106
    {
        public static string IsActive(this IHtmlHelper html, string controller, string action, string cssClass = "active")
        {
            var routeData = html.ViewContext.RouteData;
            var routeAction = routeData.Values["action"]?.ToString();
            var routeController = routeData.Values["controller"]?.ToString();
            var returnActive = controller == routeController && action == routeAction;
            return returnActive ? cssClass : string.Empty;
        }
    }
}
