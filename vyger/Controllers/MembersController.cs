using System;
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

            Member member = new Member { Email = verified.Email };

            FormsAuthenticationTicket ticket = Authenticated(member);

            string url = TempData["ReturnUrl"] as string;

            if (url.IsNullOrEmpty())
            {
                url = FormsAuthentication.GetRedirectUrl(ticket.Name, false);
            }

            return Redirect(url);
        }

        private FormsAuthenticationTicket Authenticated(Member member)
        {
            EnsureDataPath(member);

            SecurityActor sa = new SecurityActor(member);

            FormsAuthenticationTicket ticket = sa.ToAuthenticationTicket();

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            Response.Cookies.Add(cookie);

            member = SaveMember(sa);

            return ticket;
        }

        private void EnsureDataPath(Member member)
        {
            string folder = Constants.GetMemberFolder(member.Email);

            string path = Server.MapPath($"~/App_Data/{folder}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private Member SaveMember(SecurityActor sa)
        {
            IMemberService svc = new MemberService(sa);

            Member member = svc.GetMember();

            if (member == null)
            {
                member = sa.Member;
            }

            svc.SaveMember(member);

            return member;
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