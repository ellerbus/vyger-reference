using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common.Models;

namespace vyger.Tests.Common.Models
{
    [TestClass]
    public class WorkoutRoutineSetTests
    {
        [TestMethod]
        public void WorkoutRoutineSet_Display_Should_Static()
        {
            // arrange
            var set = new WorkoutRoutineSet();
            set.Weight = 135;
            set.Reps = 5;
            // act
            // assert
            set.Display.Should().Be("135x5");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Display_Should_RepMax()
        {
            // arrange
            var set = new WorkoutRoutineSet();
            set.RepMax = 12;
            // act
            // assert
            set.Display.Should().Be("12RM");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Display_Should_RepMax_With_Percent()
        {
            // arrange
            var set = new WorkoutRoutineSet();
            set.RepMax = 5;
            set.Percent = 0.80;
            // act
            // assert
            set.Display.Should().Be("5RM*80%");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Display_Should_RepMax_With_Percent_And_Reps()
        {
            // arrange
            var set = new WorkoutRoutineSet();
            set.RepMax = 1;
            set.Percent = 0.80;
            set.Reps = 5;
            // act
            // assert
            set.Display.Should().Be("1RM*80%x5");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Display_Should_RepMax_With_Percent_And_Reps_Sets()
        {
            // arrange
            var set = new WorkoutRoutineSet();
            set.RepMax = 1;
            set.Percent = 0.80;
            set.Reps = 5;
            set.Sets = 2;
            // act
            // assert
            set.Display.Should().Be("1RM*80%x5x2");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Display_Should_Reference_Set_Only_And_Percent()
        {
            // arrange
            var set = new WorkoutRoutineSet();
            set.Reference = "S5";
            set.Percent = 1.025;
            set.Reps = 3;
            // act
            // assert
            set.Display.Should().Be("S5*102.5%x3");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Display_Should_Reference_With_Percent()
        {
            // arrange
            var set = new WorkoutRoutineSet();
            set.Reference = "W1D1S5";
            set.Percent = 0.80;
            // act
            // assert
            set.Display.Should().Be("W1D1S5*80%");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Display_Should_Reference_With_Percent_And_Reps()
        {
            // arrange
            var set = new WorkoutRoutineSet();
            set.Reference = "W1D1S5";
            set.Percent = 0.80;
            set.Reps = 5;
            // act
            // assert
            set.Display.Should().Be("W1D1S5*80%x5");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_Static()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("135x5");
            // assert
            set.Display.Should().Be("135x5");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_RepMax()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("12RM");
            // assert
            set.Display.Should().Be("12RM");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_RepMax_With_Percent()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("5rm*80%");
            // assert
            set.Display.Should().Be("5RM*80%");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_RepMax_With_Percent_Alternate()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("5rm*80");
            // assert
            set.Display.Should().Be("5RM*80%");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_RepMax_With_Percent_And_Reps()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("1RM*80%x5");
            // assert
            set.Display.Should().Be("1RM*80%x5");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_RepMax_With_Percent_And_Reps_Alternate()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("1RM*80/5");
            // assert
            set.Display.Should().Be("1RM*80%x5");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_RepMax_With_Percent_And_Reps_Sets()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("1RM*80%x5x2");
            // assert
            set.Display.Should().Be("1RM*80%x5x2");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_RepMax_With_Percent_And_Reps_Sets_Alternate()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("1RM*80/5/2");
            // assert
            set.Display.Should().Be("1RM*80%x5x2");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_Reference()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("W1D2S3");
            // assert
            set.Display.Should().Be("W1D2S3");
            set.WeekId.Should().Be(1);
            set.DayId.Should().Be(2);
            set.SetId.Should().Be(3);
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_Reference_LastIndexes()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("W!D!S!");
            // assert
            set.Display.Should().Be("W!D!S!");
            set.WeekId.Should().Be(-1);
            set.DayId.Should().Be(-1);
            set.SetId.Should().Be(-1);
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_Reference_Set_Only_And_Percent()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("S5*102.5%x3");
            // assert
            set.Display.Should().Be("S5*102.5%x3");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_Reference_Set_Only_And_Percent_Alternate()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("S5*102.5/3");
            // assert
            set.Display.Should().Be("S5*102.5%x3");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_Reference_With_Percent()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("W1D1S5*80%");
            // assert
            set.Display.Should().Be("W1D1S5*80%");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_Reference_With_Percent_Alternate()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("W1D1S5*80");
            // assert
            set.Display.Should().Be("W1D1S5*80%");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_Reference_With_Percent_And_Reps()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("W1D1S5*80%x5");
            // assert
            set.Display.Should().Be("W1D1S5*80%x5");
        }

        [TestMethod]
        public void WorkoutRoutineSet_Constructor_Reference_With_Percent_And_Reps_Alternate()
        {
            // arrange
            // act
            var set = new WorkoutRoutineSet("W1D1S5*80/5");
            // assert
            set.Display.Should().Be("W1D1S5*80%x5");
        }
    }
}
