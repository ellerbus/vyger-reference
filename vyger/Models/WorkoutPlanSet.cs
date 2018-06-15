using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using vyger.Common;

namespace vyger.Models
{
    public class WorkoutPlanSet
    {
        #region Constructors

        public WorkoutPlanSet()
        {
            Reps = 1;
            Sets = 1;
        }

        public WorkoutPlanSet(WorkoutPlanExercise planExercise, WorkoutRoutineExercise routineExercise, WorkoutRoutineSet set) : this()
        {
            PlanExercise = planExercise;
            RoutineExercise = routineExercise;
            RoutineSet = set;

            Reps = set.Reps;
            Sets = set.Sets;
            CalculatedWeight = GetCalculatedWeight();
        }

        public WorkoutPlanSet(string workoutPlan) : this()
        {
            string pattern = @"(?<weight>\d+)(\s*[X/](?<reps>\d+)(\s*[X/](?<sets>\d+))?)?";

            Match match = Regex.Match(workoutPlan, pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            foreach (Group group in match.Groups.Cast<Group>().Where(x => x.Success))
            {
                switch (group.Name)
                {
                    case "weight":
                        CalculatedWeight = int.Parse(group.Value);
                        break;

                    case "reps":
                        Reps = int.Parse(group.Value);
                        break;

                    case "sets":
                        Sets = int.Parse(group.Value);
                        break;
                }
            }
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return Display;
        }

        private double GetCalculatedWeight()
        {
            if (RoutineSet.IsStatic)
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
        public int Weight
        {
            get { return (int)WorkoutCalculator.Round(CalculatedWeight); }
        }

        /// <summary>
        ///
        /// </summary>
        public int Reps { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Sets { get; set; }

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
        public string Display
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                for (int set = 0; set < Sets; set++)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(Weight).Append("x").Append(Reps);
                }

                return sb.ToString();
            }
        }

        #endregion
    }
}