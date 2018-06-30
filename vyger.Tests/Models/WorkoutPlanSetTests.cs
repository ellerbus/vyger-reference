using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Models;

namespace vyger.Tests.Models
{
    [TestClass]
    public class WorkoutPlanSetTests
    {
        [TestMethod]
        public void WorkoutPlanSet_Display_Should_BeStatic()
        {
            // arrange
            var subject = new WorkoutPlanSet();
            // act
            subject.CalculatedWeight = 132.6;
            subject.Reps = 5;
            // assert
            subject.Display.Should().Be("135x5");
        }

        [TestMethod]
        public void WorkoutPlanSet_Constructor_Should_BeStatic()
        {
            // arrange
            var subject = new WorkoutPlanSet("135x5");
            // act
            // assert
            subject.Display.Should().Be("135x5");
        }

        [TestMethod]
        public void WorkoutPlanSet_Constructor_Should_BeStatic_Alternate()
        {
            // arrange
            var subject = new WorkoutPlanSet("135/5");
            // act
            // assert
            subject.Display.Should().Be("135x5");
        }

        [TestMethod]
        public void WorkoutPlanSet_Constructor_Should_BeStatic_WithSets()
        {
            // arrange
            var subject = new WorkoutPlanSet("135x5x2");
            // act
            // assert
            subject.Display.Should().Be("135x5, 135x5");
        }

        [TestMethod]
        public void WorkoutPlanSet_Constructor_Should_BeStatic_WithSets_Alternate()
        {
            // arrange
            var subject = new WorkoutPlanSet("135/5/2");
            // act
            // assert
            subject.Display.Should().Be("135x5, 135x5");
        }
    }
}