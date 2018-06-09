using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web.Mvc;
using Augment;
using vyger.Common;
using vyger.Forms;
using vyger.Models;
using vyger.Services;

namespace vyger.Controllers
{
    [RoutePrefix("Exercises"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class ExercisesController : BaseController
    {
        #region Members

        private IExerciseService _service;
        private IExerciseGroupService _groups;
        private IExerciseCategoryService _categories;

        #endregion

        #region Constructors

        public ExercisesController(
            IExerciseService service,
            IExerciseGroupService groups,
            IExerciseCategoryService categories)
        {
            _service = service;
            _groups = groups;
            _categories = categories;
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
        public virtual ActionResult Index(string groupId = null, string categoryId = null)
        {
            IList<Exercise> exercises = _service
                .GetExercises()
                .Where(x => groupId.IsNullOrEmpty() || x.GroupId == groupId)
                .Where(x => categoryId.IsNullOrEmpty() || x.CategoryId == categoryId)
                .ToList();

            return View(exercises);
        }

        #endregion

        #region Create Methods

        [HttpGet, Route("Create")]
        public virtual ActionResult Create()
        {
            ExerciseForm form = new ExerciseForm();

            form.Groups = _groups.GetExerciseGroups();

            form.Categories = _categories.GetExerciseCategories();

            return View(form);
        }

        [HttpPost, Route("Create"), ValidateAntiForgeryToken]
        public virtual ActionResult Create(ExerciseForm post)
        {
            if (ModelState.IsValid)
            {
                _service.AddExercise(new Exercise(post));

                AddFlashMessage(FlashMessageType.Success, "Exercise created successfully");

                return RedirectToAction(MVC.Exercises.Index());
            }

            post.Groups = _groups.GetExerciseGroups();

            post.Categories = _categories.GetExerciseCategories();

            return View(post);
        }

        #endregion

        #region Edit Methods

        [HttpGet, Route("Edit/{id}")]
        public virtual ActionResult Edit(string id)
        {
            Exercise exercise = _service.GetExercises().GetByPrimaryKey(id);

            if (exercise == null)
            {
                return RedirectToAction(MVC.Exercises.Index());
            }

            ExerciseForm form = new ExerciseForm(exercise);

            form.Groups = _groups.GetExerciseGroups();

            form.Categories = _categories.GetExerciseCategories();

            return View(form);
        }

        [HttpPost, Route("Edit/{id}"), ValidateAntiForgeryToken]
        public virtual ActionResult Edit(string id, ExerciseForm post)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateExercise(id, post);

                AddFlashMessage(FlashMessageType.Success, "Exercise saved successfully");

                return RedirectToAction(MVC.Exercises.Index());
            }

            post.Groups = _groups.GetExerciseGroups();

            post.Categories = _categories.GetExerciseCategories();

            return View(post);
        }

        #endregion
    }
}
