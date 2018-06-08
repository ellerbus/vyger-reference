using System.Collections.Generic;
using System.Security;
using System.Web.Mvc;
using vyger.Common;
using vyger.Common.Models;
using vyger.Common.Services;

namespace vyger.Web.Controllers
{
    [RoutePrefix("Exercises/Groups"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class ExerciseGroupsController : BaseController<ExerciseGroupsController>
    {
        #region Members

        private ISecurityActor _actor;
        private IExerciseGroupService _service;

        #endregion

        #region Constructors

        public ExerciseGroupsController(
            ISecurityActor actor,
            IExerciseGroupService service)
        {
            _actor = actor;
            _service = service;
        }

        #endregion

        #region "On" Methods

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is SecurityException)
            {
                AddFlashMessage(FlashMessageType.Danger, filterContext.Exception.Message);

                filterContext.ExceptionHandled = true;
                filterContext.Result = RedirectToAction(MVC.ExerciseGroups.Index());
            }

            base.OnException(filterContext);
        }

        #endregion

        #region List Methods

        [HttpGet, Route("Index")]
        public virtual ActionResult Index()
        {
            IList<ExerciseGroup> groups = _service.GetExerciseGroups();

            return View(groups);
        }

        #endregion

        #region Create Methods

        [HttpGet, Route("Create")]
        public virtual ActionResult Create()
        {
            ExerciseGroup group = new ExerciseGroup();

            _actor.VerifyCan(SecurityAccess.Create, group);

            return View(group);
        }

        [HttpPost, Route("Create"), ValidateAntiForgeryToken]
        public virtual ActionResult Create(ExerciseGroup post)
        {
            _actor.VerifyCan(SecurityAccess.Create, post);

            if (ModelState.IsValid)
            {
                post.Status = StatusTypes.Active;

                _service.AddExerciseGroup(post);

                AddFlashMessage(FlashMessageType.Success, "Exercise Group created successfully");

                return RedirectToAction(MVC.ExerciseGroups.Index());
            }

            return View(post);
        }

        #endregion

        #region Edit Methods

        [HttpGet, Route("Edit/{id:int}")]
        public virtual ActionResult Edit(int id)
        {
            ExerciseGroup group = _service.GetExerciseGroup(id, SecurityAccess.View);

            if (group == null)
            {
                return RedirectToAction(MVC.ExerciseGroups.Index());
            }

            return View(group);
        }

        [HttpPost, Route("Edit/{id:int}"), ValidateAntiForgeryToken]
        public virtual ActionResult Edit(int id, ExerciseGroup post)
        {
            ExerciseGroup group = _service.GetExerciseGroup(id, SecurityAccess.Update);

            if (ModelState.IsValid)
            {
                group.OverlayWith(post);

                _service.SaveChanges();

                AddFlashMessage(FlashMessageType.Success, "Exercise Group saved successfully");

                return RedirectToAction(MVC.ExerciseGroups.Index());
            }

            return View(post);
        }

        #endregion
    }
}
