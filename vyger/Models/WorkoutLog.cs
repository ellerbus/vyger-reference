using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using Augment;

namespace vyger.Common.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public partial class WorkoutLog
    {
        #region Constructors

        public WorkoutLog() : base() { }

        public WorkoutLog(WorkoutPlanLog planLog) : this()
        {
            ExerciseId = planLog.ExerciseId;
            Exercise = planLog.Exercise;
            Workout = planLog.WorkoutPlan;
            PlanId = planLog.PlanId;
            CycleId = planLog.CycleId;
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
                string pk = $"[{MemberId}, {LogDate}, {ExerciseId}]";

                string uq = $"[{Exercise.ExerciseName},{Workout}]";

                return "{0}, pk={1}, uq={2}".FormatArgs("WorkoutLog", pk, uq);
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
        ///	Gets / Sets database column 'member_id'
        ///	</summary>
        [Required]
        [DisplayName("Member Id")]
        public override int MemberId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'log_date'
        ///	</summary>
		[Required]
        [DisplayName("Log Date")]
        public override DateTime LogDate { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'exercise_id'
        ///	</summary>
		[Required]
        [DisplayName("Exercise Id")]
        public override int ExerciseId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'workout'
        ///	</summary>
		[Required]
        [DisplayName("Workout")]
        public override string Workout
        {
            get { return base.Workout; }
            set
            {
                base.Workout = value;
                _sets = null;
            }
        }

        ///	<summary>
        ///	Gets / Sets database column 'plan_id'
        ///	</summary>
		[Required]
        [DisplayName("Plan Id")]
        public override int PlanId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'cycle_id'
        ///	</summary>
		[Required]
        [DisplayName("Cycle Id")]
        public override int CycleId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'week_id'
        ///	</summary>
		[Required]
        [DisplayName("Week Id")]
        public override int WeekId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'day_id'
        ///	</summary>
		[Required]
        [DisplayName("Day Id")]
        public override int DayId { get; set; }

        ///	<summary>
        ///	Gets / Sets database column 'sequence_number'
        ///	</summary>
		[Required]
        [DisplayName("Sequence Number")]
        public override int SequenceNumber { get; set; }

        public override DateTime CreatedAt
        {
            get { return base.CreatedAt.EnsureUtc(); }
            set { base.CreatedAt = value.EnsureUtc(); }
        }

        public override DateTime? UpdatedAt
        {
            get { return base.UpdatedAt.EnsureUtc(); }
            set { base.UpdatedAt = value.EnsureUtc(); }
        }

        [NotMapped]
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
        ///	Gets / Sets the foreign key to 'member_id'
        ///	</summary>
        [ForeignKey(nameof(MemberId))]
        public Member Member { get; set; }

        ///	<summary>
        ///	Gets / Sets the foreign key to 'exercise_id'
        ///	</summary>
        [ForeignKey(nameof(ExerciseId))]
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
