using System.Collections.Generic;
using System.Linq;
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

        #region List Methods

        [HttpGet, Route("Index")]
        public virtual ActionResult Index(string groupId = null, string categoryId = null)
        {
            IList<Exercise> exercises = _service
                .GetExercises()
                .Filter(groupId, categoryId)
                .ToList();

            ExerciseIndexViewModel vm = new ExerciseIndexViewModel()
            {
                Items = exercises,
                SelectedCategoryId = categoryId,
                SelectedGroupId = groupId
            };

            if (vm.SelectedCategoryId.IsNotEmpty())
            {
                vm.SelectedCategory = _categories.GetExerciseCategories().GetByPrimaryKey(vm.SelectedCategoryId);
            }

            if (vm.SelectedGroupId.IsNotEmpty())
            {
                vm.SelectedGroup = _groups.GetExerciseGroups().GetByPrimaryKey(vm.SelectedGroupId);
            }

            return View(vm);
        }

        #endregion

        #region Create Methods

        [HttpGet, Route("Create")]
        public virtual ActionResult Create(string groupId = null, string categoryId = null)
        {
            ExerciseDetailViewModel vm = new ExerciseDetailViewModel();

            vm.Groups = _groups.GetExerciseGroups();

            vm.Categories = _categories.GetExerciseCategories();

            if (groupId.IsNotEmpty())
            {
                ExerciseGroup group = null;

                if (_groups.GetExerciseGroups().TryGetByPrimaryKey(groupId, out group))
                {
                    vm.Group = group;
                }
            }

            if (categoryId.IsNotEmpty())
            {
                ExerciseCategory category = null;

                if (_categories.GetExerciseCategories().TryGetByPrimaryKey(categoryId, out category))
                {
                    vm.Category = category;
                }
            }

            return View(vm);
        }

        [HttpPost, Route("Create"), ValidateAntiForgeryToken]
        public virtual ActionResult Create(Exercise post)
        {
            if (ModelState.IsValid)
            {
                ExerciseCollection exercises = _service.GetExercises();

                exercises.Add(post);

                _service.SaveExercises();

                AddFlashMessage(FlashMessageType.Success, "Exercise created successfully");

                return RedirectToAction("Index");
            }

            ExerciseDetailViewModel vm = new ExerciseDetailViewModel(post);

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

            ExerciseDetailViewModel vm = new ExerciseDetailViewModel(exercise);

            vm.Groups = _groups.GetExerciseGroups();

            vm.Categories = _categories.GetExerciseCategories();

            return View(vm);
        }

        [HttpPost, Route("Edit/{id}"), ValidateAntiForgeryToken]
        public virtual ActionResult Edit(string id, Exercise post)
        {
            if (ModelState.IsValid)
            {
                ExerciseCollection exercises = _service.GetExercises();

                Exercise exercise = exercises.GetByPrimaryKey(id);

                exercise.OverlayWith(post);

                _service.SaveExercises();

                AddFlashMessage(FlashMessageType.Success, "Exercise saved successfully");

                return RedirectToAction("Index");
            }

            ExerciseDetailViewModel vm = new ExerciseDetailViewModel(post);

            vm.Groups = _groups.GetExerciseGroups();

            vm.Categories = _categories.GetExerciseCategories();

            return View(vm);
        }

        #endregion

        #region Json Query Methods

        [HttpGet, Route("Fetch")]
        public virtual ActionResult Fetch(string id)
        {
            Exercise exercise = null;

            _service.GetExercises().TryGetByPrimaryKey(id, out exercise);

            return Json(exercise ?? new Exercise(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet, Route("Query")]
        public virtual ActionResult Query(string groupId = null, string categoryId = null)
        {
            IList<Exercise> exercises = _service
                .GetExercises()
                .Filter(groupId, categoryId)
                .OrderBy(x => x.DetailName)
                .ToList();

            return Json(exercises, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
