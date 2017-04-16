using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoWeb.Filters
{
    public class DemoAuthorizeAttribute : AuthorizeAttribute
    {
        private static readonly object LockObj = new object();

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (!HttpContext.Current.User.Identity.IsAuthenticated) return;
        }
    }
}