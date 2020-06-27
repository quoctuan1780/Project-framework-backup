using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CHAdmin.Common;
using Model.EF;

namespace CHAdmin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var session = (user) Session[Common.Constants.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { Controller = "Account", action = "Login" })); 
            }
            base.OnActionExecuted(filterContext);
        }
    }
}