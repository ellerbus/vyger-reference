using System.Collections.Generic;
using vyger.Common;
using vyger.Models;

namespace vyger.Services
{
    #region Service interface

    /// <summary>
    /// Service Interface for Exercise
    /// </summary>
    public interface IExerciseService
    {
        /// <summary>
        ///
        /// </summary>
        ExerciseCollection GetExercises();

        /// <summary>
        ///
        /// </summary>
        void AddExercise(Exercise exercise);

        /// <summary>
        ///
        /// </summary>
        void UpdateExercise(string id, Exercise overlay);
    }

    #endregion

    /// <summary>
    /// Service Implementation for Exercise
    /// </summary>
    public class ExerciseService : BaseService, IExerciseService
    {
        #region Members

        private ExerciseCollection _exercises;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public ExerciseService(
            IExerciseGroupService groups,
            IExerciseCategoryService categories,
            ISecurityActor actor)
            : base(actor)
        {
            ExerciseGroupCollection grps = groups.GetExerciseGroups();

            ExerciseCategoryCollection cats = categories.GetExerciseCategories();

            IEnumerable<Exercise> exercises = ReadData<ExerciseCollection>();

            _exercises = new ExerciseCollection(grps, cats, exercises);
        }

        #endregion

        #region Methods

        /// <summary>
        ///
        /// </summary>
        public ExerciseCollection GetExercises()
        {
            return _exercises;
        }

        /// <summary>
        ///
        /// </summary>
        public void AddExercise(Exercise add)
        {
            _exercises.Add(add);

            SaveData(_exercises);
        }

        /// <summary>
        ///
        /// </summary>
        public void UpdateExercise(string id, Exercise overlay)
        {
            Exercise exercise = _exercises.GetByPrimaryKey(overlay.Id);

            exercise.OverlayWith(overlay);

            SaveData(_exercises);
        }

        #endregion

        #region Properties

        protected override string FileName
        {
            get
            {
                return typeof(Exercise).Name;
            }
        }

        #endregion
    }
}