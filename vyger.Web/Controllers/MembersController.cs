using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using vyger.Common;
using vyger.Common.Models;
using vyger.Common.Services;

namespace vyger.Web.Controllers
{
    [RoutePrefix("Members")]
    public partial class MembersController : BaseController<MembersController>
    {
        private IMemberService _service;

        public MembersController(IMemberService service)
        {
            _service = service;
        }

        [HttpGet, Route("Google"), AllowAnonymous]
        public virtual ActionResult Google(string token)
        {
            Member member = _service.AuthenticateLogin(token);

            SecurityActor sa = new SecurityActor(member);

            FormsAuthenticationTicket ticket = sa.ToAuthenticationTicket();

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            Response.Cookies.Add(cookie);

            return Redirect(FormsAuthentication.GetRedirectUrl(member.MemberId.ToString(), false));
        }

        [HttpGet, Route("Logout"), AllowAnonymous]
        public virtual ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction(MVC.Members.AccessDenied());
        }

        [HttpGet, Route("AccessDenied"), AllowAnonymous]
        public virtual ActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet, Route("Index"), MvcAuthorizeRoles(Constants.Roles.Administrator)]
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}