using vyger.Common;
using vyger.Models;

namespace vyger.Services
{
    #region Service interface

    /// <summary>
    /// Service Interface for WorkoutPlan
    /// </summary>
    public interface IWorkoutPlanService
    {
        /// <summary>
        ///
        /// </summary>
        WorkoutPlanCollection GetWorkoutPlans();

        /// <summary>
        ///
        /// </summary>
        void AddWorkoutPlan(WorkoutPlan plan);

        /// <summary>
        ///
        /// </summary>
        void UpdateWorkoutPlan(string id, WorkoutPlan overlay);

        /// <summary>
        ///
        /// </summary>
        void SaveWorkoutPlans();
    }

    #endregion

    /// <summary>
    /// Service Implementation for WorkoutPlan
    /// </summary>
    public class WorkoutPlanService : BaseService<WorkoutPlan>, IWorkoutPlanService
    {
        #region Members

        private IWorkoutRoutineService _routines;
        private WorkoutPlanCollection _plans;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public WorkoutPlanService(
            IWorkoutRoutineService routines,
            ISecurityActor actor)
            : base(actor)
        {
            _routines = routines;

            _plans = new WorkoutPlanCollection(LoadAll());

            foreach (WorkoutPlan p in _plans)
            {
                p.Routine = _routines.GetWorkoutRoutines().GetByPrimaryKey(p.RoutineId);

                //foreach (WorkoutPlanRoutine ex in p.PlanRoutines)
                //{
                //    ex.Routine = p.AllRoutines.GetByPrimaryKey(ex.RoutineId);
                //}
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///
        /// </summary>
        public WorkoutPlanCollection GetWorkoutPlans()
        {
            return _plans;
        }

        /// <summary>
        ///
        /// </summary>
        public void AddWorkoutPlan(WorkoutPlan add)
        {
            _plans.Add(add);

            SaveWorkoutPlans();
        }

        /// <summary>
        ///
        /// </summary>
        public void UpdateWorkoutPlan(string id, WorkoutPlan overlay)
        {
            WorkoutPlan plan = _plans.GetByPrimaryKey(overlay.Id);

            plan.OverlayWith(overlay);

            SaveWorkoutPlans();
        }

        public void SaveWorkoutPlans()
        {
            SaveAll(_plans);
        }

        #endregion
    }
}