namespace vyger.Core.Models
{
    public class WorkoutPlanSet : WorkoutSet
    {
        #region Constructors

        public WorkoutPlanSet() : base() { }

        public WorkoutPlanSet(string workoutPlan) : base(workoutPlan)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        protected override bool ExpandSetsOnDisplay
        {
            get { return true; }
        }

        #endregion
    }
}
