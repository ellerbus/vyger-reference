using System.Security;
using System.Web.Mvc;
using vyger.Common;
using vyger.Models;
using vyger.Services;

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

        #region "On" Methods

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is SecurityException)
            {
                AddFlashMessage(FlashMessageType.Danger, filterContext.Exception.Message);

                filterContext.ExceptionHandled = true;
                filterContext.Result = RedirectToAction(MVC.ExerciseCategories.Index());
            }

            base.OnException(filterContext);
        }

        #endregion

        #region List Methods

        [HttpGet, Route("Index")]
        public virtual ActionResult Index()
        {
            ExerciseCategoryCollection groups = _service.GetExerciseCategories();

            return View(groups);
        }

        #endregion

        #region Create Methods

        [HttpGet, Route("Create")]
        public virtual ActionResult Create()
        {
            ExerciseCategory group = new ExerciseCategory();

            return View(group);
        }

        [HttpPost, Route("Create"), ValidateAntiForgeryToken]
        public virtual ActionResult Create(ExerciseCategory post)
        {
            if (ModelState.IsValid)
            {
                _service.AddExerciseCategory(post);

                AddFlashMessage(FlashMessageType.Success, "Exercise Category created successfully");

                return RedirectToAction(MVC.ExerciseCategories.Index());
            }

            return View(post);
        }

        #endregion

        #region Edit Methods

        [HttpGet, Route("Edit/{id}")]
        public virtual ActionResult Edit(string id)
        {
            ExerciseCategory group = _service.GetExerciseCategories().GetByPrimaryKey(id);

            if (group == null)
            {
                return RedirectToAction(MVC.ExerciseCategories.Index());
            }

            return View(group);
        }

        [HttpPost, Route("Edit/{id}"), ValidateAntiForgeryToken]
        public virtual ActionResult Edit(string id, ExerciseCategory post)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateExerciseCategory(id, post);

                AddFlashMessage(FlashMessageType.Success, "Exercise Category saved successfully");

                return RedirectToAction(MVC.ExerciseCategories.Index());
            }

            return View(post);
        }

        #endregion
    }
}
