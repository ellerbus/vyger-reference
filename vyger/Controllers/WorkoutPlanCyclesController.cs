using System.Security;
using System.Web.Mvc;
using vyger.Common;
using vyger.Models;
using vyger.Services;

namespace vyger.Controllers
{
    [RoutePrefix("Workouts/Plans/{id}/Cycles"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class WorkoutPlanCyclesController : BaseController
    {
        #region Members

        private IWorkoutPlanService _service;

        #endregion

        #region Constructors

        public WorkoutPlanCyclesController(IWorkoutPlanService service)
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
        public virtual ActionResult Index(string id)
        {
            WorkoutPlan plan = _service.GetWorkoutPlans().GetByPrimaryKey(id);

            return View(plan);
        }

        #endregion

        #region Create Methods

        [HttpGet, Route("Create")]
        public virtual ActionResult Create(string id)
        {
            WorkoutPlan plan = _service.GetWorkoutPlans().GetByPrimaryKey(id);

            WorkoutPlanCycle cycle = _service.CreateCycle(plan);

            AddFlashMessage(FlashMessageType.Success, $"Cycle  created successfully");

            return RedirectToAction(MVC.WorkoutPlanExercises.Index(id, cycle.CycleId));
        }

        #endregion
    }
}