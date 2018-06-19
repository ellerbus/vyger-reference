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
    public class ExerciseService : BaseService<Exercise>, IExerciseService
    {
        #region Members

        private IExerciseGroupService _groups;
        private IExerciseCategoryService _categories;
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
            : base(actor, RepositoryTypes.Yaml)
        {
            _groups = groups;
            _categories = categories;

            _exercises = new ExerciseCollection(LoadAll());

            foreach (Exercise ex in _exercises)
            {
                ex.Group = _groups.GetExerciseGroups().GetByPrimaryKey(ex.GroupId);
                ex.Category = _categories.GetExerciseCategories().GetByPrimaryKey(ex.CategoryId);
            }
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
            add.Group = _groups.GetExerciseGroups().GetByPrimaryKey(add.GroupId);

            add.Category = _categories.GetExerciseCategories().GetByPrimaryKey(add.CategoryId);

            _exercises.Add(add);

            SaveAll(_exercises);
        }

        /// <summary>
        ///
        /// </summary>
        public void UpdateExercise(string id, Exercise overlay)
        {
            Exercise exercise = _exercises.GetByPrimaryKey(overlay.Id);

            exercise.OverlayWith(overlay);

            SaveAll(_exercises);
        }

        #endregion
    }
}