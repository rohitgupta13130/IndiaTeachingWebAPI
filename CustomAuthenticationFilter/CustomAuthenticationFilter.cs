using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace India_Teaching.CustomAuthenticationFilter
{
    public class CustomAuthenticationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Check if the user is authenticated
            if (HttpContext.Current.Session["UserSession"] == null)
            {
                // If not authenticated, redirect to the login page
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                    {
                        { "controller", "Login" },
                        { "action", "Login" }
                    });
            }
            base.OnActionExecuting(filterContext);
        }
    }
}