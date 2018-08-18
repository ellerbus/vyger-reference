using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using vyger.Core;

namespace vyger.Core.Models
{
    public class WorkoutLogSet
    {
        #region Constructors

        public WorkoutLogSet()
        {
            Reps = 1;
            Sets = 1;
        }

        public WorkoutLogSet(string workoutLog) : this()
        {
            string pattern = @"(?<weight>\d+)(\s*[X/](?<reps>\d+)(\s*[X/](?<sets>\d+))?)?";

            Match match = Regex.Match(workoutLog, pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            foreach (Group group in match.Groups.Cast<Group>().Where(x => x.Success))
            {
                switch (group.Name)
                {
                    case "weight":
                        Weight = int.Parse(group.Value);
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

        public static string Format(string workoutLog)
        {
            WorkoutLogSetCollection sets = new WorkoutLogSetCollection(workoutLog);

            return sets.Display;
        }

        public override string ToString()
        {
            return Display;
        }

        #endregion

        #region Properties

        /// <summary>
        ///
        /// </summary>
        public int Weight { get; set; }

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