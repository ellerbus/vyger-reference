using System.Collections.Generic;
using System.Security;
using System.Web.Mvc;
using vyger.Common;
using vyger.Common.Models;
using vyger.Common.Services;
using vyger.Web.Models;

namespace vyger.Web.Controllers
{
    [RoutePrefix("Workouts/Plans"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class WorkoutPlansController : BaseController<WorkoutPlansController>
    {
        #region Members

        private ISecurityActor _actor;
        private IWorkoutPlanService _service;
        private IWorkoutRoutineService _routines;

        #endregion

        #region Constructors

        public WorkoutPlansController(
            ISecurityActor actor,
            IWorkoutPlanService service,
            IWorkoutRoutineService routines)
        {
            _actor = actor;
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
            IList<WorkoutPlan> plans = _service.GetWorkoutPlans();

            return View(plans);
        }

        #endregion

        #region Create Methods

        [HttpGet, Route("Create")]
        public virtual ActionResult Create()
        {
            WorkoutPlanForm form = new WorkoutPlanForm()
            {
                CycleId = 1
            };

            _actor.VerifyCan(SecurityAccess.Create, form);

            form.Routines = _routines.GetWorkoutRoutines();

            return View(form);
        }

        [HttpPost, Route("Create"), ValidateAntiForgeryToken]
        public virtual ActionResult Create(WorkoutPlanForm post)
        {
            _actor.VerifyCan(SecurityAccess.Create, post);

            if (ModelState.IsValid)
            {
                //  force a downgrade
                _service.AddWorkoutPlan(new WorkoutPlan(post));

                AddFlashMessage(FlashMessageType.Success, "Workout Plan created successfully");

                return RedirectToAction(MVC.WorkoutPlans.Index());
            }

            post.Routines = _routines.GetWorkoutRoutines();

            return View(post);
        }

        #endregion
    }
}

