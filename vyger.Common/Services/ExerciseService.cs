using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using vyger.Common.Models;

namespace vyger.Common.Services
{
    #region Service interface

    /// <summary>
    /// Service Interface for Exercise
    /// </summary>
    public interface IExerciseService
    {
        /// <summary>
        /// Save Changes (wrapper to DbContext)
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Save Changes (wrapper to DbContext)
        /// </summary>
        /// <returns></returns>
        Exercise AddExercise(Exercise exercise);

        /// <summary>
        /// Gets a single Exercise based on the given primary key
        /// </summary>
        IList<Exercise> GetExercises(int groupId);

        /// <summary>
        /// Gets a single Exercise based on the given primary key
        /// </summary>
        Exercise GetExercise(int exerciseId, SecurityAccess access);
    }

    #endregion

    /// <summary>
    /// Service Implementation for Exercise
    /// </summary>
    public class ExerciseService : IExerciseService
    {
        #region Members

        private IVygerContext _db;
        private ISecurityActor _actor;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public ExerciseService(
            IVygerContext db,
            ISecurityActor actor)
        {
            _db = db;
            _actor = actor;
        }

        #endregion

        #region Methods

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public Exercise AddExercise(Exercise exercise)
        {
            exercise.OwnerId = _actor.MemberId;

            Exercise added = _db.Exercises.Add(exercise);

            SaveChanges();

            return added;
        }

        /// <summary>
        /// Gets a single Exercise based on the given primary key
        /// </summary>
        public IList<Exercise> GetExercises(int groupId)
        {
            IList<Exercise> exercises = _db
                .Exercises
                .WithAccessFor(_actor)
                .Where(x => groupId == 0 || x.GroupId == groupId)
                .Include(x => x.Group)
                .ToList();

            return exercises;
        }

        /// <summary>
        /// Gets a single Exercise based on the given primary key
        /// </summary>
        public Exercise GetExercise(int exerciseId, SecurityAccess access)
        {
            Exercise exercise = _db
                .Exercises
                .WithAccessFor(_actor)
                .FirstOrDefault(x => x.ExerciseId == exerciseId);

            _actor.VerifyCan(access, exercise);

            return exercise;
        }

        #endregion
    }
}
