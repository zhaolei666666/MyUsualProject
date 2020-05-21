using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImmortalBird.Controllers
{
    public class BaseController : Controller
    {
        public bool IsCheck { get; set; } = true;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (IsCheck)
            {
                if (filterContext.HttpContext.Session["username"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/Login/Login");
                }
            }
        }
    }
}