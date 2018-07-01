using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common;
using FluentAssertions;

namespace vyger.Tests.Common
{
    /// <summary>
    /// Summary description for WorkoutCalculatorTests
    /// </summary>
    [TestClass]
    public class WorkoutCalculatorTests
    {
        [TestMethod]
        public void WorkoutCalculator_InputsOutputs_Should_Match()
        {
            var weight = 65;
            var reps = 5;

            var oneRepMax = WorkoutCalculator.OneRepMax(weight, reps);

            var prediction = WorkoutCalculator.Prediction(oneRepMax, 5);

            prediction.Should().Be(weight);
        }
    }
}
