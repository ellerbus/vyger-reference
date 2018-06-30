using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Models;

namespace vyger.Tests.Models
{
    [TestClass]
    public class WorkoutCycleGeneratorTests
    {
        [TestMethod]
        public void WorkoutCycleGenerator_InitializeCycle_Should_InitializeCalculateExercise()
        {
            //  arrange
            var routine = BuildA.WorkoutRoutine(numberWeeks: 1, numberDays: 1, numberExercises: 1);

            var rex = routine.RoutineExercises.First();

            rex.WorkoutRoutine = "5RM";

            var plan = BuildA.WorkoutPlanFrom(routine);

            var cycle = plan.Cycles.First();

            var logs = new[]
            {
                new WorkoutLog { ExerciseId = rex.ExerciseId, Workout = "140x5" }
            };

            //  act
            var gen = new WorkoutCycleGenerator(plan, cycle);

            gen.InitializeCycle(logs);

            //  assert
            cycle.PlanExercises.Count().Should().Be(1);

            var pex = cycle.PlanExercises.First();

            pex.Weight.Should().Be(140);
            pex.Reps.Should().Be(5);
        }

        //[TestMethod]
        //public void WorkoutCycleGenerator_CreateExercisesForCycle_Should_FollowRoutine_WithRepMax()
        //{
        //    //  arrange
        //    var routine = BuildA.WorkoutRoutine(numberWeeks: 1, numberDays: 1, numberExercises: 1);

        //    var rex = routine.RoutineExercises.First();

        //    rex.WorkoutRoutine = "12RMx5";

        //    var plan = new WorkoutPlan(routine);

        //    var cycle = Design.One<WorkoutPlanCycle>().Build();

        //    plan.Cycles.Add(cycle);

        //    cycle.GenerateExerciseSetup(new WorkoutLog[0]);

        //    var pex = cycle.PlanExercises.First();

        //    pex.Weight = 135;
        //    pex.Reps = 5;

        //    //  act
        //    var gen = new WorkoutCycleGenerator(cycle);

        //    var logs = gen.GenerateLogItems().ToArray();

        //    //  assert
        //    logs.Count().Should().Be(1);

        //    var log = logs.First();

        //    log.WorkoutPlan.Should().Be("105x5");
        //}

        //[TestMethod]
        //public void WorkoutCycleGenerator_CreateExercisesForCycle_Should_FollowRoutine_WithRepMax_AndPercent()
        //{
        //    //  arrange
        //    var routine = BuildA.WorkoutRoutine(numberWeeks: 1, numberDays: 1, numberExercises: 1);

        //    var rex = routine.RoutineExercises.First();

        //    rex.WorkoutRoutine = "12RM*80% x5";

        //    var plan = new WorkoutPlan(routine);

        //    var cycle = Design.One<WorkoutPlanCycle>().Build();

        //    plan.Cycles.Add(cycle);

        //    cycle.GenerateExerciseSetup(new WorkoutLog[0]);

        //    var pex = cycle.PlanExercises.First();

        //    pex.Weight = 135;
        //    pex.Reps = 5;

        //    //  act
        //    var gen = new WorkoutCycleGenerator(cycle);

        //    var logs = gen.GenerateLogItems().ToArray();

        //    //  assert
        //    logs.Length.Should().Be(1);

        //    var log = logs.First();

        //    log.WorkoutPlan.Should().Be("85x5");
        //}

        //[TestMethod]
        //public void WorkoutCycleGenerator_CreateExercisesForCycle_Should_FollowRoutine_WithSameDayReference()
        //{
        //    //  arrange
        //    var routine = BuildA.WorkoutRoutine(numberWeeks: 1, numberDays: 1, numberExercises: 1);

        //    var rex = routine.RoutineExercises.First();

        //    rex.WorkoutRoutine = "S!*80% x5, S!*90% x5, 1RM*85% x5";

        //    var plan = new WorkoutPlan(routine);

        //    var cycle = Design.One<WorkoutPlanCycle>().Build();

        //    plan.Cycles.Add(cycle);

        //    cycle.GenerateExerciseSetup(new WorkoutLog[0]);

        //    var pex = cycle.PlanExercises.First();

        //    pex.Weight = 135;
        //    pex.Reps = 5;

        //    //  act
        //    var gen = new WorkoutCycleGenerator(cycle);

        //    var logs = gen.GenerateLogItems().ToArray();

        //    //  assert
        //    logs.Count().Should().Be(1);

        //    var log = logs.First();

        //    log.WorkoutPlan.Should().Be("105x5, 115x5, 130x5");
        //}

        //[TestMethod]
        //public void WorkoutCycleGenerator_CreateExercisesForCycle_Should_FollowRoutine_WithSameWeekReference()
        //{
        //    //  arrange
        //    var routine = BuildA.WorkoutRoutine(numberWeeks: 1, numberDays: 2, numberExercises: 1);

        //    var rex1 = routine.RoutineExercises[0];

        //    rex1.WorkoutRoutine = "S!*80% x5, S!*90% x5, 1RM*85% x5";

        //    var rex2 = routine.RoutineExercises[1];

        //    rex2.WorkoutRoutine = "D1S! x5";

        //    var plan = new WorkoutPlan(routine);

        //    var cycle = Design.One<WorkoutPlanCycle>().Build();

        //    plan.Cycles.Add(cycle);

        //    cycle.GenerateExerciseSetup(new WorkoutLog[0]);

        //    var pex = cycle.PlanExercises.First();

        //    pex.Weight = 135;
        //    pex.Reps = 5;

        //    //  act
        //    var gen = new WorkoutCycleGenerator(cycle);

        //    var logs = gen.GenerateLogItems().ToArray();

        //    //  assert
        //    logs.Length.Should().Be(2);

        //    var cex1 = logs.ElementAt(0);

        //    cex1.WorkoutPlan.Should().Be("105x5, 115x5, 130x5");

        //    var cex2 = logs.ElementAt(1);

        //    cex2.WorkoutPlan.Should().Be("130x5");
        //}

        //[TestMethod]
        //public void WorkoutCycleGenerator_CreateExercisesForCycle_Should_FollowRoutine_WithCycleReference()
        //{
        //    //  arrange
        //    var routine = BuildA.WorkoutRoutine(numberWeeks: 2, numberDays: 1, numberExercises: 1);

        //    var rex1 = routine.RoutineExercises[0];

        //    rex1.WorkoutRoutine = "S!*80% x5, S!*90% x5, 1RM*85% x5";

        //    var rex2 = routine.RoutineExercises[1];

        //    rex2.WorkoutRoutine = "W1D1S! x5";

        //    var plan = new WorkoutPlan(routine);

        //    var cycle = Design.One<WorkoutPlanCycle>().Build();

        //    plan.Cycles.Add(cycle);

        //    cycle.GenerateExerciseSetup(new WorkoutLog[0]);

        //    var pex = cycle.PlanExercises.First();

        //    pex.Weight = 135;
        //    pex.Reps = 5;

        //    //  act
        //    var gen = new WorkoutCycleGenerator(cycle);

        //    var logs = gen.GenerateLogItems().ToArray();

        //    //  assert
        //    logs.Length.Should().Be(2);

        //    var cex1 = logs.ElementAt(0);

        //    cex1.WorkoutPlan.Should().Be("105x5, 115x5, 130x5");

        //    var cex2 = logs.ElementAt(1);

        //    cex2.WorkoutPlan.Should().Be("130x5");
        //}
    }
}