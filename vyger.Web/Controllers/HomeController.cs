using System.Web.Mvc;
using vyger.Common;

namespace vyger.Web.Controllers
{
    [RoutePrefix("Home"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class HomeController : BaseController<HomeController>
    {
        [HttpGet, Route("Index"), Route("~/")]
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}