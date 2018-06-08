using System.Collections.Generic;
using System.Linq;
using vyger.Common.Models;

namespace vyger.Common.Services
{
    #region Service interface

    /// <summary>
    /// Service Interface for ExerciseGroup
    /// </summary>
    public interface IExerciseGroupService
    {
        /// <summary>
        /// Save Changes (wrapper to DbContext)
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ExerciseGroup AddExerciseGroup(ExerciseGroup group);

        /// <summary>
        /// Gets a single ExerciseGroup based on the given primary key
        /// </summary>
        IList<ExerciseGroup> GetExerciseGroups();

        /// <summary>
        /// Gets a single ExerciseGroup based on the given primary key
        /// </summary>
        ExerciseGroup GetExerciseGroup(int groupId, SecurityAccess access);
    }

    #endregion

    /// <summary>
    /// Service Implementation for ExerciseGroup
    /// </summary>
    public class ExerciseGroupService : IExerciseGroupService
    {
        #region Members

        private IVygerContext _db;
        private ISecurityActor _actor;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public ExerciseGroupService(
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

        public ExerciseGroup AddExerciseGroup(ExerciseGroup group)
        {
            ExerciseGroup added = _db.ExerciseGroups.Add(group);

            SaveChanges();

            return added;
        }

        /// <summary>
        /// Gets a single ExerciseGroup based on the given primary key
        /// </summary>
        public IList<ExerciseGroup> GetExerciseGroups()
        {
            IList<ExerciseGroup> groups = _db
                .ExerciseGroups
                .OrderBy(x => x.GroupName)
                .ToList();

            return groups;
        }

        /// <summary>
        /// Gets a single ExerciseGroup based on the given primary key
        /// </summary>
        public ExerciseGroup GetExerciseGroup(int groupId, SecurityAccess access)
        {
            ExerciseGroup group = _db
                .ExerciseGroups
                .FirstOrDefault(x => x.GroupId == groupId);

            _actor.VerifyCan(access, group);

            return group;
        }

        #endregion
    }
}
