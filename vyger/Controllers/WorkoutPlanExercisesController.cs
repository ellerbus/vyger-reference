using System.Security;
using System.Web.Mvc;
using vyger.Common;
using vyger.Common.Models;
using vyger.Common.Services;

namespace vyger.Web.Controllers
{
    [RoutePrefix("Workouts/Plans/{id:int}/Exercises"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class WorkoutPlanExercisesController : BaseController<WorkoutPlanExercisesController>
    {
        #region Members

        private ISecurityActor _actor;
        private IWorkoutPlanService _service;

        #endregion

        #region Constructors

        public WorkoutPlanExercisesController(
            ISecurityActor actor,
            IWorkoutPlanService service)
        {
            _actor = actor;
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
                filterContext.Result = RedirectToAction(MVC.WorkoutPlans.Index());
            }

            base.OnException(filterContext);
        }

        #endregion

        #region List Methods

        [HttpGet, Route("Index")]
        public virtual ActionResult Index(int id, int cycle)
        {
            WorkoutPlanCycle c = _service.GetWorkoutPlanCycle(id, cycle, SecurityAccess.View);

            if (c.Status == StatusTypes.None)
            {
                return View(c);
            }

            AddFlashMessage(FlashMessageType.Danger, $"Exercise Plan cannot be setup for this Cycle");

            return RedirectToAction(MVC.WorkoutPlanLogs.Index(id, cycle));
        }

        [HttpPost, Route("Index")]
        public virtual ActionResult Index(int id, int cycle, WorkoutPlanCycle post)
        {
            WorkoutPlanCycle planCycle = _service.GetWorkoutPlanCycle(id, cycle, SecurityAccess.Update);

            if (planCycle.Status == StatusTypes.None)
            {
                _service.GenerateCycle(id, cycle, post.PlanExercises);

                AddFlashMessage(FlashMessageType.Success, $"Exercise Plan Generated for Cycle {cycle}");
            }
            else
            {
                AddFlashMessage(FlashMessageType.Danger, $"Exercise Plan cannot be setup for this Cycle");
            }

            return RedirectToAction(MVC.WorkoutPlanLogs.Index(id, cycle));
        }

        #endregion
    }
}

