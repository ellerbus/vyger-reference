using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace vyger.Common.Models
{
    public class WorkoutRoutineSet
    {
        #region Constructors

        public WorkoutRoutineSet()
        {
            Percent = 1.0;
            Reps = 1;
            Sets = 1;
        }

        public WorkoutRoutineSet(string workoutRoutine) : this()
        {
            const string weight = @"((?<weight>\d+)(?<repmax>RM)?)";

            const string reference = @"(?<reference>(W(?<week>\d+|\!))?(D(?<day>\d+|\!))?S(?<set>\d+|\!))";

            string pattern = "(" + weight + "|" + reference + ")" +
                @"(\*(?<percent>\d+(\.\d+)?)\%?)?" +
                @"(\s*[X/](?<reps>\d+)(\s*[X/](?<sets>\d+))?)?";

            Match match = Regex.Match(workoutRoutine, pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            foreach (Group group in match.Groups.Cast<Group>().Where(x => x.Success))
            {
                switch (group.Name)
                {
                    case "weight":
                        Weight = int.Parse(group.Value);
                        break;
                    case "repmax":
                        Weight = 0;
                        RepMax = int.Parse(match.Groups["weight"].Value);
                        break;
                    case "reference":
                        Reference = group.Value;
                        break;
                    case "week":
                        WeekId = ParseIndex(group.Value);
                        break;
                    case "day":
                        DayId = ParseIndex(group.Value);
                        break;
                    case "set":
                        SetId = ParseIndex(group.Value);
                        break;
                    case "percent":
                        Percent = double.Parse(group.Value) / 100;
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

        private int ParseIndex(string value)
        {
            if (value == "!")
            {
                return -1;
            }

            return int.Parse(value);
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
        public bool IsStatic { get { return Weight > 0; } }

        /// <summary>
        /// 
        /// </summary>
        public int RepMax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRepMax { get { return RepMax > 0; } }

        /// <summary>
        /// 
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int WeekId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DayId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SetId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsReference { get { return !string.IsNullOrEmpty(Reference); } }

        /// <summary>
        /// 
        /// </summary>
        public double Percent { get; set; }

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
        public string Display
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                if (IsReference)
                {
                    sb.Append(Reference);
                }
                else if (IsRepMax)
                {
                    sb.Append(RepMax).Append("RM");
                }
                else
                {
                    sb.Append(Weight);
                }

                if (Percent != 1.0)
                {
                    sb.Append($"*{Percent:0.0%}".Replace(".0", ""));
                }

                if (Reps > 1)
                {
                    sb.Append("x").Append(Reps);
                }

                if (Sets > 1)
                {
                    sb.Append("x").Append(Sets);
                }

                return sb.ToString();
            }
        }

        #endregion
    }
}
