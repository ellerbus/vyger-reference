using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Core.Models;

namespace vyger.Tests.Core.Models
{
    [TestClass]
    public class ExerciseCategoryTests
    {
        #region Methods

        [TestMethod]
        public void ExerciseCategory_OverlayWith_ShouldNot_IncludePrimaryKey()
        {
            var a = Design.One<ExerciseCategory>().Build();
            var b = Design.One<ExerciseCategory>().Build();

            a.OverlayWith(b);

            a.Name.Should().Be(b.Name);

            a.Id.Should().NotBe(b.Id);
        }

        #endregion

        #region Property Tests

        #endregion

        #region Collection Tests

        [TestMethod]
        public void ExerciseCategoryCollection_GetPrimaryKey_ShouldBe_TheId()
        {
            //  arrange
            var group = Design.One<ExerciseCategory>().Build();

            var groups = new ExerciseCategoryCollection(new[] { group });

            //  act
            var actual = groups.GetByPrimaryKey(group.Id);

            //  assert
            actual.Should().BeSameAs(group);
        }

        #endregion
    }
}
