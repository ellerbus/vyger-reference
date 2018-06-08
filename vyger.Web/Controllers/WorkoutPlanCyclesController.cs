using System;
using System.Linq;
using System.Security;
using System.Web.Mvc;
using vyger.Common;
using vyger.Common.Models;
using vyger.Common.Services;

namespace vyger.Web.Controllers
{
    [RoutePrefix("Workouts/Plans/{id:int}/Cycles"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class WorkoutPlanCyclesController : BaseController<WorkoutPlanCyclesController>
    {
        #region Members

        private ISecurityActor _actor;
        private IWorkoutPlanService _service;
        private IWorkoutLogService _logs;

        #endregion

        #region Constructors

        public WorkoutPlanCyclesController(
            ISecurityActor actor,
            IWorkoutPlanService service,
            IWorkoutLogService logs)
        {
            _actor = actor;
            _service = service;
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
                filterContext.Result = RedirectToAction(MVC.WorkoutPlans.Index());
            }

            base.OnException(filterContext);
        }

        //private WorkoutPlanCycleDetailForm GetWorkoutPlanCycleDetailForm(int id)
        //{
        //    WorkoutPlanCycleDetailForm form = new WorkoutPlanCycleDetailForm()
        //    {
        //        Plan = _plans.GetWorkoutPlan(id)
        //    };

        //    return form;
        //}

        #endregion

        #region List Methods

        [HttpGet, Route("Index")]
        public virtual ActionResult Index(int id)
        {
            WorkoutPlan plan = _service.GetWorkoutPlanWithCycles(id, SecurityAccess.View);

            return View(plan);
        }

        #endregion

        #region Create Methods

        [HttpGet, Route("Create")]
        public virtual ActionResult Create(int id)
        {
            WorkoutPlanCycle cycle = _service.AddWorkoutCycle(id);

            AddFlashMessage(FlashMessageType.Success, $"Cycle  created successfully");

            return RedirectToAction(MVC.WorkoutPlanExercises.Index(id, cycle.CycleId));
        }

        #endregion
    }
}

