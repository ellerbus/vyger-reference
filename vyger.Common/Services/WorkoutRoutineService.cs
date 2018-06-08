using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EnsureThat;
using vyger.Common.Models;

namespace vyger.Common.Services
{
    #region Service interface

    /// <summary>
    /// Service Interface for WorkoutRoutine
    /// </summary>
    public interface IWorkoutRoutineService
    {
        /// <summary>
        /// Save Changes (wrapper to DbContext)
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Gets a single WorkoutRoutine based on the given primary key
        /// </summary>
        IList<WorkoutRoutine> GetWorkoutRoutines();

        /// <summary>
        /// Gets a single WorkoutRoutine based on the given primary key
        /// </summary>
        WorkoutRoutine GetWorkoutRoutine(int routineId, SecurityAccess access);

        /// <summary>
        /// Gets a single WorkoutRoutine based on the given primary key
        /// </summary>
        WorkoutRoutine GetWorkoutRoutineDisplay(int routineId, int weekId, SecurityAccess access);

        /// <summary>
        /// 
        /// </summary>
        IList<int> GetExercisesIn(int routineId, int dayId);

        /// <summary>
        /// Save Changes (wrapper to DbContext)
        /// </summary>
        /// <returns></returns>
        WorkoutRoutine AddWorkoutRoutine(WorkoutRoutine routine);

        /// <summary>
        /// 
        /// </summary>
        void AddExerciseTo(int routineId, int dayId, int exerciseId, string defaultWorkoutRoutine);

        /// <summary>
        /// 
        /// </summary>
        void RemoveExerciseFrom(int routineId, int dayId, int exerciseId);

        /// <summary>
        /// 
        /// </summary>
        void MergeExercisesWith(int routineId, int weekId, IEnumerable<WorkoutRoutineExercise> exercises);

        /// <summary>
        /// 
        /// </summary>
        void CopyForward(int routineId, int weekId, int dayId, int exerciseId);
    }

    #endregion

    /// <summary>
    /// Service Implementation for WorkoutRoutine
    /// </summary>
    public class WorkoutRoutineService : IWorkoutRoutineService
    {
        #region Members

        private IVygerContext _db;
        private ISecurityActor _actor;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public WorkoutRoutineService(
            IVygerContext db,
            ISecurityActor actor)
        {
            _db = db;
            _actor = actor;
        }

        #endregion

        #region Getter Methods

        /// <summary>
        /// Gets a single WorkoutRoutine based on the given primary key
        /// </summary>
        public IList<WorkoutRoutine> GetWorkoutRoutines()
        {
            IList<WorkoutRoutine> routines = _db
                .WorkoutRoutines
                .WithAccessFor(_actor)
                .ToList();

            return routines;
        }

        /// <summary>
        /// Gets a single WorkoutRoutine based on the given primary key
        /// </summary>
        public WorkoutRoutine GetWorkoutRoutine(int routineId, SecurityAccess access)
        {
            WorkoutRoutine routine = _db
                .WorkoutRoutines
                .WithAccessFor(_actor)
                .FirstOrDefault(x => x.RoutineId == routineId);

            _actor.VerifyCan(access, routine);

            return routine;
        }

        /// <summary>
        /// Gets a single WorkoutRoutine based on the given primary key
        /// </summary>
        public WorkoutRoutine GetWorkoutRoutineDisplay(int routineId, int weekId, SecurityAccess access)
        {
            Ensure.That(weekId, nameof(weekId)).IsInRange(Constants.MinWeeks, Constants.MaxWeeks);

            WorkoutRoutine routine = GetWorkoutRoutine(routineId, access);

            routine.RoutineExercises = _db
                .WorkoutRoutines
                .SelectMany(x => x.RoutineExercises)
                .Include(x => x.Exercise)
                .Where(x => x.RoutineId == routineId && x.WeekId == weekId)
                .OrderBy(x => x.DayId)
                .ThenBy(x => x.SequenceNumber)
                .ToList();

            return routine;
        }

        public IList<int> GetExercisesIn(int routineId, int dayId)
        {
            IList<int> ids = _db
                .WorkoutRoutines
                .SelectMany(x => x.RoutineExercises)
                .Where(x => x.RoutineId == routineId && x.DayId == dayId)
                .Select(x => x.ExerciseId)
                .Distinct()
                .ToList();

            return ids;
        }

        ///// <summary>
        ///// Gets a single WorkoutRoutine based on the given primary key
        ///// </summary>
        //public WorkoutRoutine GetWorkoutRoutineForDay(int routineId, int dayId, int exerciseId, SecurityAccess access)
        //{
        //    WorkoutRoutine routine = GetWorkoutRoutine(routineId, access);

        //    routine.RoutineExercises = _db
        //        .WorkoutRoutines
        //        .SelectMany(x => x.RoutineExercises)
        //        .Include(x => x.Exercise)
        //        .Where(x => x.RoutineId == routineId && x.DayId == dayId && x.ExerciseId == exerciseId)
        //        .ToList();

        //    return routine;
        //}

        #endregion

        #region Saver Methods

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public WorkoutRoutine AddWorkoutRoutine(WorkoutRoutine routine)
        {
            routine.OwnerId = _actor.MemberId;

            WorkoutRoutine added = _db.WorkoutRoutines.Add(routine);

            SaveChanges();

            return added;
        }

        public void AddExerciseTo(int routineId, int dayId, int exerciseId, string defaultWorkoutRoutine)
        {
            defaultWorkoutRoutine = WorkoutRoutineSetCollection.Format(defaultWorkoutRoutine);

            WorkoutRoutine routine = GetWorkoutRoutine(routineId, SecurityAccess.Update);

            routine.RoutineExercises = _db
                .WorkoutRoutines
                .SelectMany(x => x.RoutineExercises)
                .Where(x =>
                    x.RoutineId == routine.RoutineId &&
                    x.DayId == dayId &&
                    x.ExerciseId == exerciseId)
                .ToList();

            if (routine.RoutineExercises.Count == 0)
            {
                for (int w = 0; w < routine.Weeks; w++)
                {
                    WorkoutRoutineExercise ex = new WorkoutRoutineExercise()
                    {
                        RoutineId = routine.RoutineId,
                        ExerciseId = exerciseId,
                        WeekId = w + 1,
                        DayId = dayId,
                        WorkoutRoutine = defaultWorkoutRoutine,
                        SequenceNumber = 99
                    };

                    routine.RoutineExercises.Add(ex);
                }
            }

            SaveChanges();
        }

        public void RemoveExerciseFrom(int routineId, int dayId, int exerciseId)
        {
            WorkoutRoutine routine = GetWorkoutRoutine(routineId, SecurityAccess.Update);

            routine.RoutineExercises = _db
                .WorkoutRoutines
                .SelectMany(x => x.RoutineExercises)
                .Where(x =>
                    x.RoutineId == routine.RoutineId &&
                    x.DayId == dayId &&
                    x.ExerciseId == exerciseId)
                .ToList();

            routine.RoutineExercises.Clear();

            SaveChanges();
        }

        public void MergeExercisesWith(int routineId, int weekId, IEnumerable<WorkoutRoutineExercise> sources)
        {
            WorkoutRoutine routine = GetWorkoutRoutine(routineId, SecurityAccess.Update);

            routine.RoutineExercises = _db
                .WorkoutRoutines
                .SelectMany(x => x.RoutineExercises)
                .Where(x => x.RoutineId == routine.RoutineId && x.WeekId == weekId)
                .ToList();

            foreach (WorkoutRoutineExercise source in sources)
            {
                //  find all matching targets
                IEnumerable<WorkoutRoutineExercise> targets = routine
                    .RoutineExercises
                    .Where(x =>
                        x.DayId == source.DayId &&
                        x.ExerciseId == source.ExerciseId);

                foreach (WorkoutRoutineExercise target in targets)
                {
                    if (target.WeekId == source.WeekId)
                    {
                        //  the week we are merging matches the UI
                        target.OverlayWith(source);
                    }
                    else if (target.WeekId > source.WeekId)
                    {
                        //  else make sure all seq-numbers are consistant
                        target.SequenceNumber = source.SequenceNumber;
                    }
                }
            }

            SaveChanges();
        }

        public void CopyForward(int routineId, int weekId, int dayId, int exerciseId)
        {
            WorkoutRoutine routine = GetWorkoutRoutine(routineId, SecurityAccess.Update);

            routine.RoutineExercises = _db
                .WorkoutRoutines
                .SelectMany(x => x.RoutineExercises)
                .Where(x =>
                    x.RoutineId == routine.RoutineId &&
                    x.WeekId >= weekId &&
                    x.DayId == dayId &&
                    x.ExerciseId == exerciseId)
                .ToList();

            WorkoutRoutineExercise source = routine.RoutineExercises.FirstOrDefault(x => x.WeekId == weekId);

            if (source != null)
            {
                foreach (WorkoutRoutineExercise target in routine.RoutineExercises)
                {
                    if (target.WeekId > weekId)
                    {
                        target.WorkoutRoutine = source.WorkoutRoutine;
                    }
                }
            }

            SaveChanges();
        }

        #endregion
    }
}
