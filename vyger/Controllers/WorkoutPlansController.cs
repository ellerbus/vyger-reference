using System.Web.Mvc;
using vyger.Core;
using vyger.Core.Models;
using vyger.Core.Services;

namespace vyger.Controllers
{
    [RoutePrefix("Workouts/Routines/{id}/Plans"), MvcAuthorizeRoles(Constants.Roles.ActiveMember)]
    public partial class WorkoutPlansController : BaseController
    {
        #region Members

        private IPlanGeneratorService _generator;
        private IWorkoutRoutineService _service;

        #endregion

        #region Constructors

        public WorkoutPlansController(
            IPlanGeneratorService generator,
            IWorkoutRoutineService service)
        {
            _generator = generator;
            _service = service;
        }

        #endregion

        #region List Methods

        [HttpGet, Route("Index")]
        public virtual ActionResult Index(string id)
        {
            WorkoutRoutine routine = _service.GetWorkoutRoutines().GetByPrimaryKey(id);

            if (routine.Plans.Count == 0)
            {
                return RedirectToAction("Create", new { id });
            }

            return View(routine);
        }

        #endregion

        #region Create Methods

        [HttpGet, Route("Create")]
        public virtual ActionResult Create(string id)
        {
            WorkoutRoutine routine = _service.GetWorkoutRoutines().GetByPrimaryKey(id);

            return View(routine);
        }

        [HttpPost, Route("Create"), ValidateAntiForgeryToken]
        public virtual ActionResult CreatePlan(string id)
        {
            WorkoutRoutine routine = _service.GetWorkoutRoutines().GetByPrimaryKey(id);

            WorkoutPlan plan = _generator.CreateWorkoutPlan(routine);

            routine.Plans.Add(plan);

            _service.SaveWorkoutRoutines();

            return RedirectToAction("Index", new { id, plan = plan.Id });
        }

        #endregion
    }
}