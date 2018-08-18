using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Core;
using vyger.Core.Models;

namespace vyger.Tests.Core.Models
{
    [TestClass]
    public class WorkoutRoutineSetTests
    {
        [TestMethod]
        public void WorkoutRoutineSet_Constructor_Static()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("135/5");
            // assert
            set.Display.Should().Be("135/5");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_RepMax()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("12RM/5");
            // assert
            set.Display.Should().Be("12RM/5");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_RepMax_With_Percent()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("5rm80%/5");
            // assert
            set.Display.Should().Be("5RM-80%/5");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_RepMax_With_Percent_Alternate()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("5rm80%/5");
            // assert
            set.Display.Should().Be("5RM-80%/5");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_RepMax_With_Percent_And_Reps()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("1RM80%/5");
            // assert
            set.Display.Should().Be("1RM-80%/5");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_RepMax_With_Percent_And_Reps_Alternate()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("1RM-80%/5");
            // assert
            set.Display.Should().Be("1RM-80%/5");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_RepMax_With_Percent_And_Reps_Sets()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("1RM80%/5x2");
            // assert
            set.Display.Should().Be("1RM-80%/5/2");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_RepMax_With_Percent_And_Reps_Sets_Alternate()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("1RM-80%/5/2");
            // assert
            set.Display.Should().Be("1RM-80%/5/2");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_Reference()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("[123]/5");
            // assert
            set.Display.Should().Be("[123]/5");
            set.WeekId.Should().Be(1);
            set.DayId.Should().Be(2);
            set.SetId.Should().Be(3);
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_Reference_LastIndexes()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("[LLL]/5");
            // assert
            set.Display.Should().Be("[LLL]/5");
            set.WeekId.Should().Be(Constants.Referencing.Last);
            set.DayId.Should().Be(Constants.Referencing.Last);
            set.SetId.Should().Be(Constants.Referencing.Last);
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_Reference_Set_Only_And_Percent()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("[5]102.5%/3");
            // assert
            set.Display.Should().Be("[5]102.5%/3");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_Reference_With_Percent()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("[115]80%/5");
            // assert
            set.Display.Should().Be("[115]80%/5");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_Reference_With_Percent_And_Reps()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("[115]80%/5");
            // assert
            set.Display.Should().Be("[115]80%/5");
        }
    }
}
