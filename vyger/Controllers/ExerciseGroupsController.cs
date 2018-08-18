using System.Security;
using System.Web.Mvc;
using Augment;
using vyger.Core;
using vyger.Core.Models;
using vyger.Core.Services;

namespace vyger.Controllers
{
    [RoutePrefix("Exercises/Groups"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class ExerciseGroupsController : BaseController
    {
        #region Members

        private IExerciseGroupService _service;

        #endregion

        #region Constructors

        public ExerciseGroupsController(IExerciseGroupService service)
        {
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
                filterContext.Result = RedirectToAction("Index");
            }

            base.OnException(filterContext);
        }

        #endregion

        #region List Methods

        [HttpGet, Route("Index")]
        public virtual ActionResult Index()
        {
            ExerciseGroupCollection groups = _service.GetExerciseGroups();

            return View(groups);
        }

        #endregion

        #region Create Methods

        [HttpGet, Route("Create")]
        public virtual ActionResult Create()
        {
            ExerciseGroup group = new ExerciseGroup();

            return View(group);
        }

        [HttpPost, Route("Create"), ValidateAntiForgeryToken]
        public virtual ActionResult Create(ExerciseGroup post)
        {
            if (post.Id.IsNotEmpty())
            {
                bool exists = _service.GetExerciseGroups().ContainsPrimaryKey(post.Id);

                if (exists)
                {
                    ModelState.AddModelError("Id", $"ID must be unique, {post.Id} already exists");
                }
            }

            if (ModelState.IsValid)
            {
                _service.AddExerciseGroup(post);

                AddFlashMessage(FlashMessageType.Success, "Exercise Group created successfully");

                return RedirectToAction("Index");
            }

            return View(post);
        }

        #endregion

        #region Edit Methods

        [HttpGet, Route("Edit/{id}")]
        public virtual ActionResult Edit(string id)
        {
            ExerciseGroup group = _service.GetExerciseGroups().GetByPrimaryKey(id);

            if (group == null)
            {
                return RedirectToAction("Index");
            }

            return View(group);
        }

        [HttpPost, Route("Edit/{id}"), ValidateAntiForgeryToken]
        public virtual ActionResult Edit(string id, ExerciseGroup post)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateExerciseGroup(id, post);

                AddFlashMessage(FlashMessageType.Success, "Exercise Group saved successfully");

                return RedirectToAction("Index");
            }

            return View(post);
        }

        #endregion
    }
}
