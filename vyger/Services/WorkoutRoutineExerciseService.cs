using System.Collections.Generic;
using System.Linq;
using EnsureThat;
using vyger.Common.Collections;
using vyger.Common.Models;
using vyger.Common.Repositories;

namespace vyger.Common.Services
{
    #region Service interface

    /// <summary>
    /// Service Interface for WorkoutRoutineExercise
    /// </summary>
    public interface IWorkoutRoutineExerciseService
    {
        /// <summary>
        /// Gets a single WorkoutRoutineExercise based on the given primary key
        /// HEADER only
        /// </summary>
        WorkoutRoutine GetWorkoutRoutine(int routineId);

        /// <summary>
        /// AttachWorkoutRoutineExercises for a specific week / day / exercise combo (or 0 to ignore)
        /// </summary>
        void AttachWorkoutRoutineExercises(WorkoutRoutine routine, int weekId, int dayId, int exerciseId);

        /// <summary>
        /// Gets a single WorkoutRoutineExercise based on the given primary key
        /// </summary>
        WorkoutRoutineExercise GetWorkoutRoutineExercise(int routineId, int weekId, int dayId, int exerciseId);

        /// <summary>
        /// 
        /// </summary>
        WorkoutRoutineExercise SaveWorkoutRoutineExercise(WorkoutRoutineExercise exercise);

        /// <summary>
        /// 
        /// </summary>
        void SaveWorkoutRoutineExercises(IEnumerable<WorkoutRoutineExercise> routineExercises);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routine">Must have been loaded for the selected day</param>
        void AddWorkoutRoutineExercise(WorkoutRoutine routine, int dayId, int exerciseId, string workoutRoutine);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="routine">Must have been loaded for the selected day/exercise</param>
        void DeleteWorkoutRoutineExercise(WorkoutRoutine routine, int dayId, int exerciseId);
    }

    #endregion

    /// <summary>
    /// Service Implementation for WorkoutRoutineExercise
    /// </summary>
    public class WorkoutRoutineExerciseService : IWorkoutRoutineExerciseService
    {
        #region Members

        private IExerciseService _exercises;
        private IWorkoutRoutineService _routines;
        private IWorkoutRoutineExerciseRepository _repository;
        private ISecurityActor _actor;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public WorkoutRoutineExerciseService(
            IExerciseService exercises,
            IWorkoutRoutineService routines,
            IWorkoutRoutineExerciseRepository repository,
            ISecurityActor actor)
        {
            _exercises = exercises;
            _routines = routines;
            _repository = repository;
            _actor = actor;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a single WorkoutRoutineExercise based on the given primary key
        /// </summary>
        public WorkoutRoutine GetWorkoutRoutine(int routineId)
        {
            WorkoutRoutine routine = _routines.GetWorkoutRoutine(routineId);

            return routine;
        }

        #endregion

        #region Attach Methods

        /// <summary>
        /// Gets a single WorkoutRoutineExercise based on the given primary key
        /// </summary>
        public void AttachWorkoutRoutineExercises(WorkoutRoutine routine, int weekId, int dayId, int exerciseId)
        {
            if (exerciseId > 0)
            {
                routine.AllExercises = new ExerciseCollection(new[] { _exercises.GetExercise(exerciseId) });
            }
            else
            {
                routine.AllExercises = _exercises.GetExercises(0);
            }

            routine.RoutineExercises.WeekId = weekId;

            routine.RoutineExercises.DayId = dayId;

            routine.RoutineExercises.ExerciseId = exerciseId;

            routine.RoutineExercises.AddRange(_repository.SelectMany(routine.RoutineId, weekId, dayId, exerciseId));
        }

        #endregion

        #region CRUD Methods

        /// <summary>
        /// Gets a single WorkoutRoutineExercise based on the given primary key
        /// </summary>
        public WorkoutRoutineExercise GetWorkoutRoutineExercise(int routineId, int weekId, int dayId, int exerciseId)
        {
            WorkoutRoutineExercise exercise = _repository.SelectOne(routineId, weekId, dayId, exerciseId);

            return exercise;
        }

        public void AddWorkoutRoutineExercise(WorkoutRoutine routine, int dayId, int exerciseId, string workoutRoutine)
        {
            workoutRoutine = WorkoutRoutineSetCollection.Format(workoutRoutine);

            Exercise exercise = routine.AllExercises.GetByPrimaryKey(exerciseId);

            List<WorkoutRoutineExercise> routineExercises = new List<WorkoutRoutineExercise>();

            for (int w = 0; w < routine.Weeks; w++)
            {
                WorkoutRoutineExercise routineExercise = new WorkoutRoutineExercise()
                {
                    Routine = routine,
                    Exercise = exercise,
                    WeekId = w + 1,
                    DayId = dayId,
                    WorkoutRoutine = workoutRoutine,
                    SequenceNumber = 99
                };

                _actor.EnsureAudit(routineExercise);

                routineExercises.Add(routineExercise);
            }

            _repository.InsertMany(routineExercises);
        }

        /// <summary>
        /// Saves a WorkoutRoutineExercise
        /// </summary>
        /// <returns></returns>
        public void DeleteWorkoutRoutineExercise(WorkoutRoutine routine, int dayId, int exerciseId)
        {
            Exercise exercise = routine.AllExercises.GetByPrimaryKey(exerciseId);

            IList<WorkoutRoutineExercise> routineExercises = _repository.SelectMany(routine.RoutineId, 0, dayId, exerciseId);

            _repository.DeleteMany(routineExercises);
        }

        /// <summary>
        /// Saves a WorkoutRoutineExercise
        /// </summary>
        /// <returns></returns>
        public WorkoutRoutineExercise SaveWorkoutRoutineExercise(WorkoutRoutineExercise temp)
        {
            WorkoutRoutineExercise exercise = OverlayCache(temp);

            if (exercise.IsModified)
            {
                _actor.EnsureAudit(exercise);

                _repository.Update(exercise);
            }

            return exercise;
        }

        /// <summary>
        /// Saves a WorkoutRoutineExercise
        /// </summary>
        /// <returns></returns>
        public void SaveWorkoutRoutineExercises(IEnumerable<WorkoutRoutineExercise> exercises)
        {
            _repository.UpdateMany(exercises);
        }

        /// <summary>
        /// OverlayCache for WorkoutRoutineExercise
        /// </summary>
        /// <returns></returns>
        private WorkoutRoutineExercise OverlayCache(WorkoutRoutineExercise temp)
        {
            WorkoutRoutineExercise exercise = GetWorkoutRoutineExercise(temp.RoutineId, temp.WeekId, temp.DayId, temp.ExerciseId);

            if (exercise == null)
            {
                exercise = temp;
            }
            else
            {
                exercise.OverlayWith(temp);
            }

            return exercise;
        }

        #endregion
    }
}
