using vyger.Core.Models;

namespace vyger.Core.Repositories
{
    public interface IExerciseCategoryRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ExerciseCategoryCollection GetExerciseCategories();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categories"></param>
        void SaveExerciseCategories(ExerciseCategoryCollection categories);
    }

    class ExerciseCategoryRepository : BaseRepository, IExerciseCategoryRepository
    {
        #region Constructors

        public ExerciseCategoryRepository(ISecurityActor actor) : base(actor)
        {

        }

        #endregion

        #region Interface

        public ExerciseCategoryCollection GetExerciseCategories()
        {
            return ReadData<ExerciseCategoryCollection>();
        }

        public void SaveExerciseCategories(ExerciseCategoryCollection categories)
        {
            SaveData(categories);
        }

        #endregion

        #region Properties

        protected override string FileName
        {
            get
            {
                return typeof(ExerciseCategory).Name;
            }
        }

        #endregion
    }
}
