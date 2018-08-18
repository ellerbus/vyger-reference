namespace vyger.Core.Models
{
    public class WorkoutPlanSet : WorkoutSet
    {
        #region Constructors

        public WorkoutPlanSet(WorkoutPlanExercise planExercise, WorkoutRoutineExercise routineExercise, WorkoutRoutineSet set) : base()
        {
            PlanExercise = planExercise;
            RoutineExercise = routineExercise;
            RoutineSet = set;

            Reps = set.Reps;
            Sets = set.Sets;
            CalculatedWeight = GetCalculatedWeight();
        }

        public WorkoutPlanSet() : base()
        {
        }

        public WorkoutPlanSet(string workoutPlan) : base(workoutPlan)
        {
        }

        #endregion

        #region Methods

        private double GetCalculatedWeight()
        {
            if (RoutineSet.IsStaticWeight)
            {
                return RoutineSet.Weight;
            }

            if (RoutineSet.IsRepMax)
            {
                return WorkoutCalculator.Prediction(PlanExercise.OneRepMax, RoutineSet.RepMax) * RoutineSet.Percent;
            }

            return double.NaN;
        }

        #endregion

        #region Properties

        /// <summary>
        ///
        /// </summary>
        public WorkoutPlanExercise PlanExercise { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public WorkoutRoutineExercise RoutineExercise { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public WorkoutRoutineSet RoutineSet { get; private set; }

        /// <summary>
        /// Whether or not a plan is being built
        /// </summary>
        public bool IsBuildingPlan
        {
            get
            {
                return PlanExercise != null || RoutineExercise != null || RoutineSet != null;
            }
        }

        /// <summary>
        /// The real calculated weight (vs display or plate weight)
        /// </summary>
        public double CalculatedWeight { get; set; }

        /// <summary>
        ///
        /// </summary>
        public override int Weight
        {
            get { return (int)WorkoutCalculator.Round(CalculatedWeight); }
            set { CalculatedWeight = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public double OneRepMax
        {
            get { return WorkoutCalculator.OneRepMax(Weight, Reps); }
        }

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