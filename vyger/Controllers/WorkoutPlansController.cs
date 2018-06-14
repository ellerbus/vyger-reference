using System.Security;
using System.Web.Mvc;
using vyger.Common;
using vyger.Forms;
using vyger.Models;
using vyger.Services;

namespace vyger.Controllers
{
    [RoutePrefix("Workouts/Plans"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class WorkoutPlansController : BaseController
    {
        #region Members

        private IWorkoutPlanService _service;
        private IWorkoutRoutineService _routines;

        #endregion

        #region Constructors

        public WorkoutPlansController(IWorkoutPlanService service, IWorkoutRoutineService routines)
        {
            _service = service;
            _routines = routines;
        }

        #endregion

        #region "On" Methods

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is SecurityException)
            {
                AddFlashMessage(FlashMessageType.Danger, filterContext.Exception.Message);

                filterContext.ExceptionHandled = true;
                filterContext.Result = RedirectToAction(MVC.WorkoutPlans.Index());
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
            WorkoutPlanForm form = new WorkoutPlanForm()
            {
                Routines = _routines.GetWorkoutRoutines()
            };

            return View(form);
        }

        [HttpPost, Route("Create"), ValidateAntiForgeryToken]
        public virtual ActionResult Create(WorkoutPlanForm post)
        {
            if (ModelState.IsValid)
            {
                WorkoutRoutine routine = _routines.GetWorkoutRoutines().GetByPrimaryKey(post.RoutineId);

                _service.AddWorkoutPlan(new WorkoutPlan(routine));

                AddFlashMessage(FlashMessageType.Success, "Workout Plan created successfully");

                return RedirectToAction(MVC.WorkoutPlans.Index());
            }

            post.Routines = _routines.GetWorkoutRoutines();

            return View(post);
        }

        #endregion
    }
}