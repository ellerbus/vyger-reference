using vyger.Core.Models;

namespace vyger.Core.Repositories
{
    public interface IWorkoutPlanRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        WorkoutPlanCollection GetWorkoutPlans();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plans"></param>
        void SaveWorkoutPlans(WorkoutPlanCollection plans);
    }

    class WorkoutPlanRepository : BaseRepository, IWorkoutPlanRepository
    {
        #region Constructors

        public WorkoutPlanRepository(ISecurityActor actor) : base(actor)
        {

        }

        #endregion

        #region Interface

        public WorkoutPlanCollection GetWorkoutPlans()
        {
            return ReadData<WorkoutPlanCollection>();
        }

        public void SaveWorkoutPlans(WorkoutPlanCollection plans)
        {
            SaveData(plans);
        }

        #endregion

        #region Properties

        protected override string FileName
        {
            get
            {
                return typeof(WorkoutPlan).Name;
            }
        }

        #endregion
    }
}
