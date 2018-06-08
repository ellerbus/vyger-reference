using System.Linq;
using System.Web.Mvc;
using Augment;

namespace vyger.Web
{
    public class MvcAuthorizeRolesAttribute : AuthorizeAttribute
    {
        public MvcAuthorizeRolesAttribute(params string[] roles) : base()
        {
            Roles = roles.Join(",");
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                //filterContext.Result = new RedirectResult("~/Members/AccessDenied");
            }
        }
    }
}