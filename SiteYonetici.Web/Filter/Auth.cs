using SiteYonetici.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteYonetici.Web.Filter
{
    public class Auth : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentSession.Manager == null)
            {
                filterContext.Result = new RedirectResult("/Manager/Login");
            }
        }
    }
}