using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using vyger.Common.Models;

namespace vyger.Tests.Common.Models
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

        //[TestMethod]
        //public void Exercise_ExerciseId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var exercise = new Exercise() { ExerciseId = 1 };
        //
        //	exercise.ExerciseId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void Exercise_OwnerId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var exercise = new Exercise() { OwnerId = 1 };
        //
        //	exercise.OwnerId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void Exercise_ExerciseName_Should_DoSomething()
        //{
        //  var expected = "aa";
        //
        //	var exercise = new Exercise() { ExerciseName = "aa" };
        //
        //	exercise.ExerciseName.Should().Be(expected);
        //}

        //[TestMethod]
        //public void Exercise_GroupId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var exercise = new Exercise() { GroupId = 1 };
        //
        //	exercise.GroupId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void Exercise_StatusEnum_Should_DoSomething()
        //{
        //  var expected = "aa";
        //
        //	var exercise = new Exercise() { StatusEnum = "aa" };
        //
        //	exercise.StatusEnum.Should().Be(expected);
        //}

        //[TestMethod]
        //public void Exercise_CreatedAt_Should_DoSomething()
        //{
        //  var expected = DateTime.Now;
        //
        //	var exercise = new Exercise() { CreatedAt = DateTime.Now };
        //
        //	exercise.CreatedAt.Should().Be(expected);
        //}

        //[TestMethod]
        //public void Exercise_UpdatedAt_Should_DoSomething()
        //{
        //  var expected = DateTime.Now;
        //
        //	var exercise = new Exercise() { UpdatedAt = DateTime.Now };
        //
        //	exercise.UpdatedAt.Should().Be(expected);
        //}

        #endregion
    }
}
