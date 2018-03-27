using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace MyBankMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                string cookieName = FormsAuthentication.FormsCookieName;
                HttpCookie authCookie = Context.Request.Cookies[cookieName];
                if (authCookie == null)
                    return;
            }
        }
        void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            string CookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authcookie = Context.Request.Cookies[CookieName];
            if (authcookie == null)
                return;
            FormsAuthenticationTicket authticket = null;
            try
            {
                authticket = FormsAuthentication.Decrypt(authcookie.Value);
            }
            catch (Exception)
            {
                return;
            }

            if (authticket == null)
                return;
            string roles = authticket.UserData;
            string[] RoleArray = roles.Split(new char[] { '|' });
            HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(User.Identity, RoleArray);
        }
    }
}
