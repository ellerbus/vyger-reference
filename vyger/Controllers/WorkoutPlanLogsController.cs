using System.Security;
using System.Web.Mvc;
using vyger.Common;
using vyger.Models;
using vyger.Services;

namespace vyger.Controllers
{
    [RoutePrefix("Workouts/Plans/{id}/Logs"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class WorkoutPlanLogsController : BaseController
    {
        #region Members

        private IWorkoutPlanService _service;

        #endregion

        #region Constructors

        public WorkoutPlanLogsController(IWorkoutPlanService service)
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
            WorkoutPlan plan = _service.GetWorkoutPlans().GetByPrimaryKey(id);

            WorkoutPlanCycle planCycle = plan.Cycles.GetByPrimaryKey(cycle);

            return View(planCycle);
        }

        #endregion
    }
}