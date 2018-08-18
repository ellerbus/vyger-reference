using System.Security;
using System.Web.Mvc;
using vyger.Core;
using vyger.Core.Models;
using vyger.Core.Services;
using vyger.ViewModels;

namespace vyger.Controllers
{
    [RoutePrefix("Workouts/Plans"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class WorkoutPlansController : BaseController
    {
        #region Members

        private IWorkoutPlanService _service;
        private IWorkoutRoutineService _routines;
        private IWorkoutLogService _logs;

        #endregion

        #region Constructors

        public WorkoutPlansController(
            IWorkoutPlanService service,
            IWorkoutRoutineService routines,
            IWorkoutLogService logs)
        {
            _service = service;
            _routines = routines;
            _logs = logs;
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
            WorkoutPlanCollection plans = _service.GetWorkoutPlans();

            return View(plans);
        }

        #endregion

        #region Create Methods

        [HttpGet, Route("Create")]
        public virtual ActionResult Create()
        {
            WorkoutPlanViewModel vm = new WorkoutPlanViewModel()
            {
                Routines = _routines.GetWorkoutRoutines()
            };

            return View(vm);
        }

        [HttpPost, Route("Create"), ValidateAntiForgeryToken]
        public virtual ActionResult Create(WorkoutPlanViewModel post)
        {
            if (ModelState.IsValid)
            {
                WorkoutRoutine routine = _routines.GetWorkoutRoutines().GetByPrimaryKey(post.RoutineId);

                WorkoutPlan plan = new WorkoutPlan(routine);

                WorkoutLogCollection logs = _logs.GetWorkoutLogs();

                _service.CreateCycle(plan, logs.GetRecentWorkoutLogs(plan.Id, 1));

                _service.AddWorkoutPlan(plan);

                AddFlashMessage(FlashMessageType.Success, "Workout Plan created successfully");

                return RedirectToAction("Index", "WorkoutPlanExercises", new { id = plan.Id, cycle = 1 });
            }

            post.Routines = _routines.GetWorkoutRoutines();

            return View(post);
        }

        #endregion
    }
}