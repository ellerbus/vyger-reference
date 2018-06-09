using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common.Models;

namespace vyger.Tests.Common.Models
{
    [TestClass]
    public class WorkoutLogSetTests
    {
        [TestMethod]
        public void WorkoutLogSet_Display_Should_BeStatic()
        {
            // arrange
            var subject = new WorkoutLogSet();
            // act
            subject.Weight = 135;
            subject.Reps = 5;
            // assert
            subject.Display.Should().Be("135x5");
        }

        [TestMethod]
        public void WorkoutLogSet_Constructor_Should_BeStatic()
        {
            // arrange
            var subject = new WorkoutLogSet("135x5");
            // act
            // assert
            subject.Display.Should().Be("135x5");
        }

        [TestMethod]
        public void WorkoutLogSet_Constructor_Should_BeStatic_Alternate()
        {
            // arrange
            var subject = new WorkoutLogSet("135/5");
            // act
            // assert
            subject.Display.Should().Be("135x5");
        }

        [TestMethod]
        public void WorkoutLogSet_Constructor_Should_BeStatic_WithSets()
        {
            // arrange
            var subject = new WorkoutLogSet("135x5x2");
            // act
            // assert
            subject.Display.Should().Be("135x5, 135x5");
        }

        [TestMethod]
        public void WorkoutLogSet_Constructor_Should_BeStatic_WithSets_Alternate()
        {
            // arrange
            var subject = new WorkoutLogSet("135/5/2");
            // act
            // assert
            subject.Display.Should().Be("135x5, 135x5");
        }
    }
}