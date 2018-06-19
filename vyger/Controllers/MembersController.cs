using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Augment;
using EnsureThat;
using vyger.Common;
using vyger.Models;
using vyger.Services;

namespace vyger.Controllers
{
    [RoutePrefix("Members")]
    public partial class MembersController : BaseController
    {
        #region Members

        private IAuthenticationService _authentication;

        #endregion

        #region Constructors

        public MembersController(IAuthenticationService authentication)
        {
            _authentication = authentication;
        }

        #endregion

        #region Authentication

        [HttpGet, Route("Google"), AllowAnonymous]
        public virtual ActionResult Google(string token)
        {
            AuthenticationToken verified = _authentication.VerifyGoogleAuthentication(token);

            Ensure.That(verified, nameof(verified)).IsNotNull();

            FormsAuthenticationTicket ticket = Authenticated(new SecurityActor(verified.Email));

            string url = TempData["ReturnUrl"] as string;

            if (url.IsNullOrEmpty())
            {
                url = FormsAuthentication.GetRedirectUrl(ticket.Name, false);
            }

            return Redirect(url);
        }

        private FormsAuthenticationTicket Authenticated(SecurityActor sa)
        {
            EnsureDataPath(sa);

            FormsAuthenticationTicket ticket = sa.ToAuthenticationTicket();

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            Response.Cookies.Add(cookie);

            return ticket;
        }

        private void EnsureDataPath(SecurityActor sa)
        {
            string folder = Constants.GetMemberFolder(sa.Email);

            string path = Server.MapPath($"~/App_Data/{folder}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        #endregion

        #region Methods

        [HttpGet, Route("Logout"), AllowAnonymous]
        public virtual ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction(MVC.Home.Index());
        }

        [HttpGet, Route("AccessDenied"), AllowAnonymous]
        public virtual ActionResult AccessDenied()
        {
            TempData["ReturnUrl"] = Request.QueryString["ReturnUrl"];

            return View();
        }

        [HttpGet, Route("Index"), MvcAuthorizeRoles(Constants.Roles.Administrator)]
        public virtual ActionResult Index()
        {
            return View();
        }

        #endregion
    }
}