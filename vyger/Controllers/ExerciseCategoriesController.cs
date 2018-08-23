using System.Web.Mvc;
using Augment;
using vyger.Core;
using vyger.Core.Models;
using vyger.Core.Services;

namespace vyger.Controllers
{
    [RoutePrefix("Exercises/Categories"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class ExerciseCategoriesController : BaseController
    {
        #region Members

        private IExerciseCategoryService _service;

        #endregion

        #region Constructors

        public ExerciseCategoriesController(IExerciseCategoryService service)
        {
            _service = service;
        }

        #endregion

        #region List Methods

        [HttpGet, Route("Index")]
        public virtual ActionResult Index()
        {
            ExerciseCategoryCollection categories = _service.GetExerciseCategories();

            return View(categories);
        }

        #endregion

        #region Create Methods

        [HttpGet, Route("Create")]
        public virtual ActionResult Create()
        {
            ExerciseCategory category = new ExerciseCategory();

            return View(category);
        }

        [HttpPost, Route("Create"), ValidateAntiForgeryToken]
        public virtual ActionResult Create(ExerciseCategory post)
        {
            if (post.Id.IsNotEmpty())
            {
                bool exists = _service.GetExerciseCategories().ContainsPrimaryKey(post.Id);

                if (exists)
                {
                    ModelState.AddModelError("Id", $"ID must be unique, {post.Id} already exists");
                }
            }

            if (ModelState.IsValid)
            {
                ExerciseCategoryCollection categories = _service.GetExerciseCategories();

                categories.Add(post);

                _service.SaveExerciseCategories();

                AddFlashMessage(FlashMessageType.Success, "Exercise Category created successfully");

                return RedirectToAction("Index");
            }

            return View(post);
        }

        #endregion

        #region Edit Methods

        [HttpGet, Route("Edit/{id}")]
        public virtual ActionResult Edit(string id)
        {
            ExerciseCategory category = _service.GetExerciseCategories().GetByPrimaryKey(id);

            if (category == null)
            {
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpPost, Route("Edit/{id}"), ValidateAntiForgeryToken]
        public virtual ActionResult Edit(string id, ExerciseCategory post)
        {
            if (ModelState.IsValid)
            {
                ExerciseCategoryCollection categories = _service.GetExerciseCategories();

                ExerciseCategory category = categories.GetByPrimaryKey(id);

                category.OverlayWith(post);

                _service.SaveExerciseCategories();

                AddFlashMessage(FlashMessageType.Success, "Exercise Category saved successfully");

                return RedirectToAction("Index");
            }

            return View(post);
        }

        #endregion
    }
}
