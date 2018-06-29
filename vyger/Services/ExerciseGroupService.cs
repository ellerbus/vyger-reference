using vyger.Common;
using vyger.Models;

namespace vyger.Services
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
    public class ExerciseGroupService : BaseService, IExerciseGroupService
    {
        #region Members

        private ExerciseGroupCollection _groups;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public ExerciseGroupService(ISecurityActor actor)
            : base(actor)
        {
            _groups = ReadData<ExerciseGroupCollection>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///
        /// </summary>
        public ExerciseGroupCollection GetExerciseGroups()
        {
            return _groups;
        }

        /// <summary>
        ///
        /// </summary>
        public void AddExerciseGroup(ExerciseGroup add)
        {
            add.Id = add.Name.Substring(0, 1);

            _groups.Add(add);

            SaveData(_groups);
        }

        /// <summary>
        ///
        /// </summary>
        public void UpdateExerciseGroup(string id, ExerciseGroup overlay)
        {
            ExerciseGroup group = _groups.GetByPrimaryKey(overlay.Id);

            group.OverlayWith(overlay);

            SaveData(_groups);
        }

        #endregion

        #region Properties

        protected override string FileName
        {
            get
            {
                return typeof(ExerciseGroup).Name;
            }
        }

        #endregion
    }
}