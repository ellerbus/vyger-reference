using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common;
using vyger.Common.Collections;
using vyger.Common.Models;

namespace vyger.Tests.Common.Models
{
    [TestClass]
    public class WorkoutRoutineTests
    {
        #region Constructor Tests

        [TestMethod]
        public void WorkoutRoutine_EmptyConstructor_Should_FlagModified()
        {
            var routine = new WorkoutRoutine()
            {
                RoutineId = 1,
                OwnerId = 1,
                RoutineName = "aa",
                Weeks = 1,
                Days = 1,
                StatusEnum = "aa",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            routine.IsModified.Should().BeTrue();
        }

        [TestMethod]
        public void WorkoutRoutine_FullConstructor_Should_FlagNotModified()
        {
            var routine = new WorkoutRoutine(
                routineId: 1,
                ownerId: 1,
                routineName: "aa",
                weeks: 1,
                days: 1,
                statusEnum: "aa",
                createdAt: DateTime.Now,
                updatedAt: DateTime.Now
                );

            routine.IsModified.Should().BeFalse();
        }

        #endregion

        #region Methods

        [TestMethod]
        public void WorkoutRoutine_OverlayWith_ShouldNot_IncludePrimaryKey()
        {
            var a = Design.One<WorkoutRoutine>().Build();
            var b = Design.One<WorkoutRoutine>().Build();

            a.OverlayWith(b);

            a.RoutineName.Should().Be(b.RoutineName);
            a.Weeks.Should().Be(b.Weeks);
            a.Days.Should().Be(b.Days);
            a.StatusEnum.Should().Be(b.StatusEnum);

            a.RoutineId.Should().NotBe(b.RoutineId);
            a.OwnerId.Should().NotBe(b.OwnerId);
            a.CreatedAt.Should().NotBe(b.CreatedAt);
            a.UpdatedAt.Should().NotBe(b.UpdatedAt.GetValueOrDefault());
        }

        #endregion

        #region Property Tests

        [TestMethod]
        public void WorkoutRoutine_StatusEnum_Should_Default()
        {
            var routine = new WorkoutRoutine();

            routine.Status.Should().Be(StatusTypes.None);
        }

        #endregion

        #region Collection Tests

        [TestMethod]
        public void WorkoutRoutineCollection_GetPrimaryKey_ShouldBe_TheId()
        {
            //  arrange
            var routine = Design.One<WorkoutRoutine>().Build();

            var routines = new WorkoutRoutineCollection(new[] { routine });

            //  act
            var actual = routines.GetByPrimaryKey(routine.RoutineId);

            //  assert
            actual.Should().BeSameAs(routine);
        }

        #endregion
    }
}
