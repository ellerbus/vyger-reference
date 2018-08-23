using Augment;
using Augment.Caching;
using vyger.Core.Models;
using vyger.Core.Repositories;

namespace vyger.Core.Services
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
        void SaveExerciseCategories();
    }

    #endregion

    /// <summary>
    /// Service Implementation for ExerciseCategory
    /// </summary>
    public class ExerciseCategoryService : IExerciseCategoryService
    {
        #region Members

        private ISecurityActor _actor;
        private IExerciseCategoryRepository _repository;
        private ICacheManager _cache;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public ExerciseCategoryService(
            ISecurityActor actor,
            IExerciseCategoryRepository repository,
            ICacheManager cache)
        {
            _actor = actor;
            _repository = repository;
            _cache = cache;
        }

        #endregion

        #region Methods

        /// <summary>
        ///
        /// </summary>
        public ExerciseCategoryCollection GetExerciseCategories()
        {
            ExerciseCategoryCollection categories = _cache
                .Cache(() => _repository.GetExerciseCategories())
                .By("Actor", _actor.Email)
                .DurationOf(5.Minutes())
                .CachedObject;

            return categories;
        }

        /// <summary>
        ///
        /// </summary>
        public void SaveExerciseCategories()
        {
            ExerciseCategoryCollection categories = GetExerciseCategories();

            _repository.SaveExerciseCategories(categories);
        }

        #endregion
    }
}