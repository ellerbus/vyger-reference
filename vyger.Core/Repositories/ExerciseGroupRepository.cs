using vyger.Core.Models;

namespace vyger.Core.Repositories
{
    public interface IExerciseGroupRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ExerciseGroupCollection GetExerciseGroups();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groups"></param>
        void SaveExerciseGroups(ExerciseGroupCollection groups);
    }

    class ExerciseGroupRepository : BaseRepository, IExerciseGroupRepository
    {
        #region Constructors

        public ExerciseGroupRepository(ISecurityActor actor) : base(actor)
        {

        }

        #endregion

        #region Interface

        public ExerciseGroupCollection GetExerciseGroups()
        {
            return ReadData<ExerciseGroupCollection>();
        }

        public void SaveExerciseGroups(ExerciseGroupCollection groups)
        {
            SaveData(groups);
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
