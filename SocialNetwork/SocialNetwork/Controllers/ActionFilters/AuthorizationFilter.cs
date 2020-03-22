using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Controllers.ActionFilters
{
    public class AuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public string RedirectUrl = "~/Views/Account/Login";

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
            || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                {
                    // Don't check for authorization as AllowAnonymous filter is applied to the action or controller
                    return;
                }

            if (HttpContext.Current.Session["username"] == null)
            {
                filterContext.Result = new RedirectResult(RedirectUrl);
            }
        }
    }
}