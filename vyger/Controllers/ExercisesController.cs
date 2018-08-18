using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web.Mvc;
using Augment;
using vyger.Core;
using vyger.Core.Models;
using vyger.Core.Services;
using vyger.ViewModels;

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
                filterContext.Result = RedirectToAction("Index");
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
            ExerciseViewModel vm = new ExerciseViewModel();

            vm.Groups = _groups.GetExerciseGroups();

            vm.Categories = _categories.GetExerciseCategories();

            return View(vm);
        }

        [HttpPost, Route("Create"), ValidateAntiForgeryToken]
        public virtual ActionResult Create(Exercise post)
        {
            if (ModelState.IsValid)
            {
                _service.AddExercise(post);

                AddFlashMessage(FlashMessageType.Success, "Exercise created successfully");

                return RedirectToAction("Index");
            }

            ExerciseViewModel vm = new ExerciseViewModel(post);

            vm.Groups = _groups.GetExerciseGroups();

            vm.Categories = _categories.GetExerciseCategories();

            return View(vm);
        }

        #endregion

        #region Edit Methods

        [HttpGet, Route("Edit/{id}")]
        public virtual ActionResult Edit(string id)
        {
            Exercise exercise = _service.GetExercises().GetByPrimaryKey(id);

            if (exercise == null)
            {
                return RedirectToAction("Index");
            }

            ExerciseViewModel vm = new ExerciseViewModel(exercise);

            vm.Groups = _groups.GetExerciseGroups();

            vm.Categories = _categories.GetExerciseCategories();

            return View(vm);
        }

        [HttpPost, Route("Edit/{id}"), ValidateAntiForgeryToken]
        public virtual ActionResult Edit(string id, Exercise post)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateExercise(id, post);

                AddFlashMessage(FlashMessageType.Success, "Exercise saved successfully");

                return RedirectToAction("Index");
            }

            ExerciseViewModel vm = new ExerciseViewModel(post);

            vm.Groups = _groups.GetExerciseGroups();

            vm.Categories = _categories.GetExerciseCategories();

            return View(vm);
        }

        #endregion

        #region Query Methods

        [HttpGet, Route("Query")]
        public virtual ActionResult Query(string id)
        {
            Exercise exercise = null;

            _service.GetExercises().TryGetByPrimaryKey(id, out exercise);

            return Json(exercise ?? new Exercise(), JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
