using System.Security;
using System.Web.Mvc;
using vyger.Common;
using vyger.Models;
using vyger.Services;

namespace vyger.Controllers
{
    [RoutePrefix("Workouts/Plans/{id}/Exercises"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class WorkoutPlanExercisesController : BaseController
    {
        #region Members

        private IWorkoutPlanService _service;

        #endregion

        #region Constructors

        public WorkoutPlanExercisesController(IWorkoutPlanService service)
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
                filterContext.Result = RedirectToAction(MVC.WorkoutPlans.Index());
            }

            base.OnException(filterContext);
        }

        #endregion

        #region List Methods

        [HttpGet, Route("Index")]
        public virtual ActionResult Index(string id, int cycle)
        {
            WorkoutPlanCycle planCycle = _service.GetWorkoutPlans()
                .GetByPrimaryKey(id)
                .Cycles
                .GetByPrimaryKey(cycle);

            //if (c.Status == StatusTypes.None)
            //{
            //    return View(planCycle);
            //}

            //AddFlashMessage(FlashMessageType.Danger, $"Exercise Plan cannot be setup for this Cycle");

            //return RedirectToAction(MVC.WorkoutPlanLogs.Index(id, cycle));

            return View(planCycle);
        }

        [HttpPost, Route("Index")]
        public virtual ActionResult Index(string id, int cycle, WorkoutPlanCycle post)
        {
            WorkoutPlanCycle planCycle = _service.GetWorkoutPlans()
                .GetByPrimaryKey(id)
                .Cycles
                .GetByPrimaryKey(cycle);

            if (planCycle.Status == StatusTypes.None)
            {
                //_service.GenerateCycle(id, cycle, post.PlanExercises);

                AddFlashMessage(FlashMessageType.Success, $"Exercise Plan Generated for Cycle {cycle}");
            }
            else
            {
                AddFlashMessage(FlashMessageType.Danger, $"Exercise Plan cannot be setup for this Cycle");
            }

            return View(planCycle);
            //return RedirectToAction(MVC.WorkoutPlanLogs.Index(id, cycle));
        }

        #endregion
    }
}