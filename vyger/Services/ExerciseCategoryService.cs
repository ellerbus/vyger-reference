using vyger.Common;
using vyger.Models;

namespace vyger.Services
{
    #region Service interface

    /// <summary>
    /// Service Interface for ExerciseCategory
    /// </summary>
    public interface IExerciseCategoryService
    {
        /// <summary>
        ///
        /// </summary>
        ExerciseCategoryCollection GetExerciseCategories();

        /// <summary>
        ///
        /// </summary>
        void AddExerciseCategory(ExerciseCategory category);

        /// <summary>
        ///
        /// </summary>
        void UpdateExerciseCategory(string id, ExerciseCategory overlay);
    }

    #endregion

    /// <summary>
    /// Service Implementation for ExerciseCategory
    /// </summary>
    public class ExerciseCategoryService : BaseService<ExerciseCategory>, IExerciseCategoryService
    {
        #region Members

        private ExerciseCategoryCollection _categories;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public ExerciseCategoryService(ISecurityActor actor)
            : base(actor, RepositoryTypes.Yaml)
        {
            _categories = new ExerciseCategoryCollection(LoadAll());
        }

        #endregion

        #region Methods

        /// <summary>
        ///
        /// </summary>
        public ExerciseCategoryCollection GetExerciseCategories()
        {
            return _categories;
        }

        /// <summary>
        ///
        /// </summary>
        public void AddExerciseCategory(ExerciseCategory add)
        {
            add.Id = add.Name.Substring(0, 1);

            _categories.Add(add);

            SaveAll(_categories);
        }

        /// <summary>
        ///
        /// </summary>
        public void UpdateExerciseCategory(string id, ExerciseCategory overlay)
        {
            ExerciseCategory category = _categories.GetByPrimaryKey(overlay.Id);

            category.OverlayWith(overlay);

            SaveAll(_categories);
        }

        #endregion
    }
}