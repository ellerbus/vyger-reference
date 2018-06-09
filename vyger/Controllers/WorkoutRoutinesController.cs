using System.Security;
using System.Web.Mvc;
using vyger.Common;
using vyger.Forms;
using vyger.Models;
using vyger.Services;

namespace vyger.Controllers
{
    [RoutePrefix("Workouts/Routines"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class WorkoutRoutinesController : BaseController
    {
        #region Members

        private IWorkoutRoutineService _service;

        #endregion

        #region Constructors

        public WorkoutRoutinesController(IWorkoutRoutineService service)
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
                filterContext.Result = RedirectToAction(MVC.WorkoutRoutines.Index());
            }

            base.OnException(filterContext);
        }

        #endregion

        #region List Methods

        [HttpGet, Route("Index")]
        public virtual ActionResult Index()
        {
            WorkoutRoutineCollection routines = _service.GetWorkoutRoutines();

            return View(routines);
        }

        #endregion

        #region Create Methods

        [HttpGet, Route("Create")]
        public virtual ActionResult Create()
        {
            WorkoutRoutineForm form = new WorkoutRoutineForm();

            const string ids = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            int idx = _service.GetWorkoutRoutines().Count;

            form.Id = ids[idx].ToString();

            return View(form);
        }

        [HttpPost, Route("Create"), ValidateAntiForgeryToken]
        public virtual ActionResult Create(WorkoutRoutineForm post)
        {
            if (ModelState.IsValid)
            {
                WorkoutRoutine routine = new WorkoutRoutine(post);

                _service.AddWorkoutRoutine(routine);

                AddFlashMessage(FlashMessageType.Success, "Workout Routine created successfully");

                return RedirectToAction(MVC.WorkoutRoutineExercises.Index(routine.Id, 1));
            }

            return View(post);
        }

        #endregion

        #region Edit Methods

        [HttpGet, Route("Edit/{id}")]
        public virtual ActionResult Edit(string id)
        {
            WorkoutRoutine routine = _service.GetWorkoutRoutines().GetByPrimaryKey(id);

            if (routine == null)
            {
                return RedirectToAction(MVC.WorkoutRoutines.Index());
            }

            WorkoutRoutineForm form = new WorkoutRoutineForm(routine);

            return View(form);
        }

        [HttpPost, Route("Edit/{id}"), ValidateAntiForgeryToken]
        public virtual ActionResult Edit(string id, WorkoutRoutine post)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateWorkoutRoutine(id, post);

                AddFlashMessage(FlashMessageType.Success, "Workout Routine saved successfully");

                return RedirectToAction(MVC.WorkoutRoutineExercises.Index(id, 1));
            }

            return View(post);
        }

        #endregion
    }
}

