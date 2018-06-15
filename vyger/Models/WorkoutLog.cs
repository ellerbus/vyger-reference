using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using Augment;

namespace vyger.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class WorkoutLog
    {
        #region Constructors

        public WorkoutLog() : base()
        {
        }

        public WorkoutLog(WorkoutPlanLog planLog) : this()
        {
            ExerciseId = planLog.PlanExercise.ExerciseId;
            Exercise = planLog.PlanExercise.Exercise;
            Workout = planLog.WorkoutPlan;
            PlanId = planLog.Cycle.Plan.Id;
            CycleId = planLog.Cycle.CycleId;
            WeekId = planLog.WeekId;
            DayId = planLog.DayId;
            SequenceNumber = planLog.SequenceNumber;
        }

        #endregion

        #region ToString/DebuggerDisplay

        public override string ToString()
        {
            return DebuggerDisplay;
        }

        ///	<summary>
        ///	DebuggerDisplay for this object
        ///	</summary>
        private string DebuggerDisplay
        {
            get
            {
                string id = $"[{LogDate:d}, {ExerciseId}]";

                string nm = $"[{Exercise?.Name},{Workout}]";

                return "{0}, id={1}, nm={2}".FormatArgs("WorkoutLog", id, nm);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///	Overlay all properties (except primary key, audits)
        /// </summary>
        public void OverlayWith(WorkoutLog other)
        {
            Workout = other.Workout;
            PlanId = other.PlanId;
            CycleId = other.CycleId;
            WeekId = other.WeekId;
            DayId = other.DayId;
            SequenceNumber = other.SequenceNumber;
            Workout = other.Workout;
        }

        #endregion

        #region Properties

        ///	<summary>
        ///	Gets / Sets database column 'log_date'
        ///	</summary>
        [Required]
        [DisplayName("Log Date")]
        public DateTime LogDate { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'exercise_id'
        ///	</summary>
        [Required]
        [DisplayName("Exercise Id")]
        public string ExerciseId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'workout'
        ///	</summary>
        [Required]
        [DisplayName("Workout")]
        public string Workout
        {
            get { return _workout; }
            set
            {
                _workout = value;
                _sets = null;
            }
        }

        private string _workout;

        ///	<summary>
        ///	Gets / Sets database column 'plan_id'
        ///	</summary>
        [Required]
        [DisplayName("Plan Id")]
        public string PlanId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'cycle_id'
        ///	</summary>
        [Required]
        [DisplayName("Cycle Id")]
        public int CycleId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'week_id'
        ///	</summary>
        [Required]
        [DisplayName("Week Id")]
        public int WeekId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'day_id'
        ///	</summary>
        [Required]
        [DisplayName("Day Id")]
        public int DayId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'sequence_number'
        ///	</summary>
        [Required]
        [DisplayName("Sequence Number")]
        public int SequenceNumber { get; set; }

        /// <summary>
        ///
        /// </summary>
        public double OneRepMax
        {
            get
            {
                if (Sets.Count > 0)
                {
                    return Sets.Max(x => x.OneRepMax);
                }

                return double.NaN;
            }
        }

        #endregion

        #region Foreign Key Properties

        ///	<summary>
        ///	Gets / Sets the foreign key to 'exercise_id'
        ///	</summary>
        public Exercise Exercise { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public WorkoutLogSetCollection Sets
        {
            get
            {
                if (_sets == null)
                {
                    _sets = new WorkoutLogSetCollection(Workout);
                }
                return _sets;
            }
        }

        private WorkoutLogSetCollection _sets;

        #endregion
    }
}