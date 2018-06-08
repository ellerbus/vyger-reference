using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common.Models;

namespace vyger.Tests.Common.Models
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

            a.GroupName.Should().Be(b.GroupName);
            a.StatusEnum.Should().Be(b.StatusEnum);

            a.GroupId.Should().NotBe(b.GroupId);
            a.CreatedAt.Should().NotBe(b.CreatedAt);
            a.UpdatedAt.Should().NotBe(b.UpdatedAt.GetValueOrDefault());
        }

        #endregion

        #region Property Tests

        //[TestMethod]
        //public void ExerciseGroup_GroupId_Should_DoSomething()
        //{
        //  var expected = 1;
        //
        //	var group = new ExerciseGroup() { GroupId = 1 };
        //
        //	group.GroupId.Should().Be(expected);
        //}

        //[TestMethod]
        //public void ExerciseGroup_GroupName_Should_DoSomething()
        //{
        //  var expected = "aa";
        //
        //	var group = new ExerciseGroup() { GroupName = "aa" };
        //
        //	group.GroupName.Should().Be(expected);
        //}

        //[TestMethod]
        //public void ExerciseGroup_StatusEnum_Should_DoSomething()
        //{
        //  var expected = "aa";
        //
        //	var group = new ExerciseGroup() { StatusEnum = "aa" };
        //
        //	group.StatusEnum.Should().Be(expected);
        //}

        //[TestMethod]
        //public void ExerciseGroup_CreatedAt_Should_DoSomething()
        //{
        //  var expected = DateTime.Now;
        //
        //	var group = new ExerciseGroup() { CreatedAt = DateTime.Now };
        //
        //	group.CreatedAt.Should().Be(expected);
        //}

        //[TestMethod]
        //public void ExerciseGroup_UpdatedAt_Should_DoSomething()
        //{
        //  var expected = DateTime.Now;
        //
        //	var group = new ExerciseGroup() { UpdatedAt = DateTime.Now };
        //
        //	group.UpdatedAt.Should().Be(expected);
        //}

        #endregion
    }
}
