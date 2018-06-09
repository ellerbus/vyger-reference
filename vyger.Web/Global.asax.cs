using System;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace vyger.Web
{
    public class Global : HttpApplication
    {
        #region Members

        private static readonly Container _container = new Injector(GetPrincipal, new WebRequestLifestyle());

        #endregion

        #region Events

        protected void Application_Start(object sender, EventArgs e)
        {
            MvcConfig.Register();

            // This is an extension method from the integration package.
            _container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            _container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(_container));

            FluentValidationModelValidatorProvider.Configure();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            ExceptionLogging.LogException(ex);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        private void Application_AuthenticateRequest(object sender, EventArgs eventArgs)
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity != null)
                {
                    IIdentity id = HttpContext.Current.User.Identity;

                    if (id.IsAuthenticated)
                    {
                        HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                        FormsAuthenticationTicket decodedTicket = FormsAuthentication.Decrypt(cookie.Value);

                        string[] roles = decodedTicket.UserData.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                        GenericPrincipal principal = new GenericPrincipal(id, roles);

                        HttpContext.Current.User = principal;
                    }
                }
            }
        }

        #endregion

        #region Static

        private static IPrincipal GetPrincipal()
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                IPrincipal p = HttpContext.Current.User;

                if (p.Identity != null)
                {
                    return p;
                }
            }

            return null;
        }

        #endregion
    }
}