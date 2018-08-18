using Augment;
using Augment.Caching;
using vyger.Core.Models;
using vyger.Core.Repositories;

namespace vyger.Core.Services
{
    #region Service interface

    /// <summary>
    /// Service Interface for ExerciseGroup
    /// </summary>
    public interface IExerciseGroupService
    {
        /// <summary>
        ///
        /// </summary>
        ExerciseGroupCollection GetExerciseGroups();

        /// <summary>
        ///
        /// </summary>
        void AddExerciseGroup(ExerciseGroup group);

        /// <summary>
        ///
        /// </summary>
        void UpdateExerciseGroup(string id, ExerciseGroup overlay);
    }

    #endregion

    /// <summary>
    /// Service Implementation for ExerciseGroup
    /// </summary>
    public class ExerciseGroupService : IExerciseGroupService
    {
        #region Members

        private ISecurityActor _actor;
        private IExerciseGroupRepository _repository;
        private ICacheManager _cache;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public ExerciseGroupService(
            ISecurityActor actor,
            IExerciseGroupRepository repository,
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
        public ExerciseGroupCollection GetExerciseGroups()
        {
            ExerciseGroupCollection groups = _cache
                .Cache(() => _repository.GetExerciseGroups())
                .By("Actor", _actor.Email)
                .DurationOf(5.Minutes())
                .CachedObject;

            return groups;
        }

        /// <summary>
        ///
        /// </summary>
        public void AddExerciseGroup(ExerciseGroup add)
        {
            ExerciseGroupCollection groups = GetExerciseGroups();

            groups.Add(add);

            _repository.SaveExerciseGroups(groups);
        }

        /// <summary>
        ///
        /// </summary>
        public void UpdateExerciseGroup(string id, ExerciseGroup overlay)
        {
            ExerciseGroupCollection groups = GetExerciseGroups();

            ExerciseGroup group = groups.GetByPrimaryKey(overlay.Id);

            group.OverlayWith(overlay);

            _repository.SaveExerciseGroups(groups);
        }

        #endregion
    }
}