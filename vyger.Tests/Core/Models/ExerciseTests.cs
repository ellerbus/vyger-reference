using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Core;
using vyger.Core.Models;

namespace vyger.Tests.Core.Models
{
    [TestClass]
    public class ExerciseTests
    {
        #region Methods

        [TestMethod]
        public void Exercise_OverlayWith_ShouldNot_IncludePrimaryKey()
        {
            var a = Design.One<Exercise>().Build();
            var b = Design.One<Exercise>().Build();

            a.OverlayWith(b);

            a.Name.Should().Be(b.Name);

            a.CategoryId.Should().NotBe(b.CategoryId);
            a.GroupId.Should().NotBe(b.GroupId);
            a.Id.Should().NotBe(b.Id);
        }

        #endregion

        #region Property Tests

        [TestMethod]
        public void Exercise_GroupId_Should_UseBase()
        {
            var exercise = new Exercise() { GroupId = "1" };

            exercise.GroupId.Should().Be("1");
        }

        [TestMethod]
        public void Exercise_GroupId_Should_UseReference()
        {
            var exercise = new Exercise()
            {
                GroupId = "1",
                Group = Design.One<ExerciseGroup>().Build()
            };

            exercise.GroupId.Should().Be(exercise.Group.Id);
        }

        #endregion

        #region Collection Tests

        [TestMethod]
        public void ExerciseCollection_GetPrimaryKey_ShouldBe_TheId()
        {
            //  arrange
            var exercise = Design.One<Exercise>().Build();

            var exercises = new ExerciseCollection(null, null, new[] { exercise });

            //  act
            var actual = exercises.GetByPrimaryKey(exercise.Id);

            //  assert
            actual.Should().BeSameAs(exercise);
        }

        [TestMethod]
        public void ExerciseCollection_UpdateReferences_Should_SetGroupAndCategory()
        {
            //  arrange
            var groups = BuildAn.ExerciseGroupCollection(2);

            var categories = BuildAn.ExerciseCategoryCollection(2);

            var exercise = Design.One<Exercise>().Build();

            exercise.GroupId = groups.Last().Id;

            exercise.CategoryId = categories.Last().Id;

            var exercises = new ExerciseCollection(groups, categories, new[] { exercise });

            //  act
            var actual = exercises.GetByPrimaryKey(exercise.Id);

            //  assert
            actual.Should().BeSameAs(exercise);

            actual.Group.Should().BeSameAs(groups.Last());

            actual.Category.Should().BeSameAs(categories.Last());
        }

        #endregion
    }
}
