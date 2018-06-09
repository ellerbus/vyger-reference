using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Augment;
using vyger.Common.Collections;

namespace vyger.Common.Models
{
    ///	<summary>
    ///
    ///	</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public partial class WorkoutRoutine
    {
        #region Constructors

        public WorkoutRoutine() { }

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
                string pk = $"[{RoutineId}]";

                string uq = $"[{OwnerId}, {RoutineName}]";

                return "{0}, pk={1}, uq={2}".FormatArgs("WorkoutRoutine", pk, uq);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///	Overlay all properties (except primary key, audits)
        /// </summary>
        public void OverlayWith(WorkoutRoutine other)
        {
            RoutineName = other.RoutineName;
            Weeks = other.Weeks;
            Days = other.Days;
            Status = other.Status;
        }

        #endregion

        #region Properties

        ///	<summary>
        ///	Gets / Sets database column 'routine_id'
        ///	</summary>
        [Required]
        [DisplayName("Routine Id")]
        public override int RoutineId
        {
            get { return base.RoutineId; }
            set { base.RoutineId = value; }
        }

        ///	<summary>
        ///	Gets / Sets database column 'owner_id'
        ///	</summary>
		[Required]
        [DisplayName("Owner Id")]
        public override int OwnerId
        {
            get { return base.OwnerId; }
            set { base.OwnerId = value; }
        }

        ///	<summary>
        ///	Gets / Sets database column 'routine_name'
        ///	</summary>
		[Required]
        [DisplayName("Routine Name")]
        public override string RoutineName
        {
            get { return base.RoutineName; }
            set { base.RoutineName = value; }
        }

        ///	<summary>
        ///	Gets / Sets database column 'weeks'
        ///	</summary>
		[Required]
        [DisplayName("Weeks")]
        [Range(Constants.MinWeeks, Constants.MaxWeeks)]
        public override int Weeks
        {
            get { return base.Weeks; }
            set { base.Weeks = value; }
        }

        ///	<summary>
        ///	Gets / Sets database column 'days'
        ///	</summary>
		[Required]
        [DisplayName("Days")]
        [Range(Constants.MinDays, Constants.MaxDays)]
        public override int Days
        {
            get { return base.Days; }
            set { base.Days = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public StatusTypes Status
        {
            get { return StatusEnum.ToEnum<StatusTypes>(); }
            set { StatusEnum = value.ToString(); }
        }

        public override string StatusEnum
        {
            get { return base.StatusEnum.AssertNotNull(StatusTypes.None.ToString()); }
            set { base.StatusEnum = value; }
        }

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

        #endregion

        #region Foreign Key Properties

        /// <summary>
        /// 
        /// </summary>
        public ExerciseCollection AllExercises { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public WorkoutRoutineExerciseCollection RoutineExercises
        {
            get
            {
                return _routineExercises = (_routineExercises ?? new WorkoutRoutineExerciseCollection(this));
            }
        }
        private WorkoutRoutineExerciseCollection _routineExercises;

        #endregion
    }
}
