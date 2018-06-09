using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Models;

namespace vyger.Tests.Models
{
    [TestClass]
    public class ExerciseGroupTests
    {
        #region Methods

        [TestMethod]
        public void ExerciseGroup_OverlayWith_ShouldNot_IncludePrimaryKey()
        {
            var a = Design.One<ExerciseGroup>().Build();
            var b = Design.One<ExerciseGroup>().Build();

            a.OverlayWith(b);

            a.Name.Should().Be(b.Name);

            a.Id.Should().NotBe(b.Id);
        }

        #endregion

        #region Property Tests

        #endregion

        #region Collection Tests

        [TestMethod]
        public void ExerciseGroupCollection_GetPrimaryKey_ShouldBe_TheId()
        {
            //  arrange
            var group = Design.One<ExerciseGroup>().Build();

            var groups = new ExerciseGroupCollection(new[] { group });

            //  act
            var actual = groups.GetByPrimaryKey(group.Id);

            //  assert
            actual.Should().BeSameAs(group);
        }

        #endregion
    }
}
