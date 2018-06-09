using System.Security;
using System.Web.Mvc;
using vyger.Common;
using vyger.Common.Models;
using vyger.Common.Services;
using vyger.Web.Models;

namespace vyger.Web.Controllers
{
    [RoutePrefix("Workouts/Plans/{id:int}/Logs"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class WorkoutPlanLogsController : BaseController<WorkoutPlanLogsController>
    {
        #region Members

        private ISecurityActor _actor;
        private IWorkoutPlanService _service;
        private IWorkoutRoutineService _routines;

        #endregion

        #region Constructors

        public WorkoutPlanLogsController(
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
        public virtual ActionResult Index(int id, int cycle)
        {
            WorkoutPlanLogForm form = new WorkoutPlanLogForm()
            {
                Cycle = _service.GetWorkoutPlanCycle(id, cycle, SecurityAccess.View)
            };

            form.Routine = _routines.GetWorkoutRoutine(form.Cycle.Plan.RoutineId, SecurityAccess.View);

            return View(form);
        }

        #endregion
    }
}

