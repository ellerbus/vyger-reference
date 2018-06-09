using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common;
using vyger.Common.Collections;
using vyger.Common.Models;

namespace vyger.Tests.Common.Models
{
    [TestClass]
    public class ExerciseTests
    {
        #region Constructor Tests

        [TestMethod]
        public void Exercise_EmptyConstructor_Should_FlagModified()
        {
            var exercise = new Exercise()
            {
                ExerciseId = 1,
                OwnerId = 1,
                ExerciseName = "aa",
                GroupId = 1,
                StatusEnum = "aa",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            exercise.IsModified.Should().BeTrue();
        }

        [TestMethod]
        public void Exercise_FullConstructor_Should_FlagNotModified()
        {
            var exercise = new Exercise(
                exerciseId: 1,
                ownerId: 1,
                exerciseName: "aa",
                groupId: 1,
                statusEnum: "aa",
                createdAt: DateTime.Now,
                updatedAt: DateTime.Now
                );

            exercise.IsModified.Should().BeFalse();
        }

        #endregion

        #region Methods

        [TestMethod]
        public void Exercise_OverlayWith_ShouldNot_IncludePrimaryKey()
        {
            var a = Design.One<Exercise>().Build();
            var b = Design.One<Exercise>().Build();

            a.OverlayWith(b);

            a.ExerciseName.Should().Be(b.ExerciseName);
            a.GroupId.Should().Be(b.GroupId);
            a.StatusEnum.Should().Be(b.StatusEnum);

            a.ExerciseId.Should().NotBe(b.ExerciseId);
            a.OwnerId.Should().NotBe(b.OwnerId);
            a.CreatedAt.Should().NotBe(b.CreatedAt);
            a.UpdatedAt.Should().NotBe(b.UpdatedAt.GetValueOrDefault());
        }

        #endregion

        #region Property Tests

        [TestMethod]
        public void Exercise_GroupId_Should_UseBase()
        {
            var exercise = new Exercise() { GroupId = 1 };

            exercise.GroupId.Should().Be(1);
        }

        [TestMethod]
        public void Exercise_GroupId_Should_UseReference()
        {
            var exercise = new Exercise()
            {
                GroupId = 1,
                Group = Design.One<ExerciseGroup>().Build()
            };

            exercise.GroupId.Should().Be(exercise.Group.GroupId);
        }

        [TestMethod]
        public void Exercise_StatusEnum_Should_Default()
        {
            var exercise = new Exercise();

            exercise.Status.Should().Be(StatusTypes.None);
        }

        #endregion

        #region Collection Tests

        [TestMethod]
        public void ExerciseCollection_GetPrimaryKey_ShouldBe_TheId()
        {
            //  arrange
            var exercise = Design.One<Exercise>().Build();

            var exercises = new ExerciseCollection(new[] { exercise });

            //  act
            var actual = exercises.GetByPrimaryKey(exercise.ExerciseId);

            //  assert
            actual.Should().BeSameAs(exercise);
        }

        #endregion
    }
}
