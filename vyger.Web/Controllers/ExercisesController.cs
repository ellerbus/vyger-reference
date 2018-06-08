using System.Collections.Generic;
using System.Security;
using System.Web.Mvc;
using vyger.Common;
using vyger.Common.Models;
using vyger.Common.Services;
using vyger.Web.Models;

namespace vyger.Web.Controllers
{
    [RoutePrefix("Exercises"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class ExercisesController : BaseController<ExercisesController>
    {
        #region Members

        private ISecurityActor _actor;
        private IExerciseService _service;
        private IExerciseGroupService _groups;

        #endregion

        #region Constructors

        public ExercisesController(
            ISecurityActor actor,
            IExerciseService service,
            IExerciseGroupService groups)
        {
            _actor = actor;
            _service = service;
            _groups = groups;
        }

        #endregion

        #region "On" Methods

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is SecurityException)
            {
                AddFlashMessage(FlashMessageType.Danger, filterContext.Exception.Message);

                filterContext.ExceptionHandled = true;
                filterContext.Result = RedirectToAction(MVC.Exercises.Index());
            }

            base.OnException(filterContext);
        }

        #endregion

        #region List Methods

        [HttpGet, Route("Index")]
        public virtual ActionResult Index(int groupId = 0)
        {
            IList<Exercise> exercises = _service.GetExercises(groupId);

            return View(exercises);
        }

        #endregion

        #region Create Methods

        [HttpGet, Route("Create")]
        public virtual ActionResult Create()
        {
            ExerciseForm form = new ExerciseForm();

            _actor.VerifyCan(SecurityAccess.Create, form);

            form.Groups = _groups.GetExerciseGroups();

            return View(form);
        }

        [HttpPost, Route("Create"), ValidateAntiForgeryToken]
        public virtual ActionResult Create(ExerciseForm post)
        {
            _actor.VerifyCan(SecurityAccess.Create, post);

            if (ModelState.IsValid)
            {
                post.Status = StatusTypes.Active;

                _service.AddExercise(post);

                AddFlashMessage(FlashMessageType.Success, "Exercise Group created successfully");

                return RedirectToAction(MVC.Exercises.Index());
            }

            post.Groups = _groups.GetExerciseGroups();

            return View(post);
        }

        #endregion

        #region Edit Methods

        [HttpGet, Route("Edit/{id:int}")]
        public virtual ActionResult Edit(int id)
        {
            Exercise exercise = _service.GetExercise(id, SecurityAccess.Update);

            if (exercise == null)
            {
                return RedirectToAction("Index", "ExerciseGroups");
            }

            ExerciseForm form = new ExerciseForm(exercise)
            {
                Groups = _groups.GetExerciseGroups()
            };

            return View(form);
        }

        [HttpPost, Route("Edit/{id:int}"), ValidateAntiForgeryToken]
        public virtual ActionResult Edit(int id, ExerciseForm post)
        {
            Exercise exercise = _service.GetExercise(id, SecurityAccess.Update);

            if (ModelState.IsValid)
            {
                exercise.OverlayWith(post);

                _service.SaveChanges();

                AddFlashMessage(FlashMessageType.Success, "Exercise Group saved successfully");

                return RedirectToAction(MVC.Exercises.Index());
            }

            post.Groups = _groups.GetExerciseGroups();

            return View(post);
        }

        #endregion
    }
}
