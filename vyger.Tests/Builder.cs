using FizzWare.NBuilder;
using FizzWare.NBuilder.PropertyNaming;

namespace vyger.Tests
{
    static class Design
    {
        static Design()
        {
            BuilderSettings settings = new BuilderSettings()
            {
                AutoNameProperties = true
            };

            BuilderSetup.SetDefaultPropertyName(new RandomValuePropertyNamer(settings));
        }

        public static ISingleObjectBuilder<T> One<T>()
        {
            return Builder<T>.CreateNew();
        }

        public static IListBuilder<T> Many<T>(int size = 5)
        {
            return Builder<T>.CreateListOfSize(size);
        }
    }

    //    static class BuilderExtensions
    //    {
    //        public static ExerciseCollection AsCollection(this IListBuilder<Exercise> builder)
    //        {
    //            return new ExerciseCollection(builder.Build());
    //        }

    //        public static ExerciseGroupCollection AsCollection(this IListBuilder<ExerciseGroup> builder)
    //        {
    //            return new ExerciseGroupCollection(builder.Build());
    //        }
    //    }


    //    static class BuildA
    //    {
    //        public static WorkoutRoutine Routine()
    //        {
    //            var routine = Design.One<WorkoutRoutine>()
    //                .With(x => x.Weeks = 4)
    //                .And(x => x.Days = 3)
    //                .Build();

    //            var exercise = Design.One<Exercise>()
    //                .With(x => x.Group = Design.One<ExerciseGroup>().Build())
    //                .Build();

    //            routine.AllExercises = new ExerciseCollection(new[] { exercise });

    //            var routineExercises = Design.One<WorkoutRoutineExercise>()
    //                .With(x => x.ExerciseId = exercise.ExerciseId)
    //                .Build();

    //            routine.RoutineExercises.Add(routineExercises);

    //            return routine;
    //        }
    //        //public static WorkoutRoutine CompleteRoutine(int numberWeeks = 4, int numberDays = 3, int numberExercises = 5)
    //        //{
    //        //    var routine = Design.One<WorkoutRoutine>()
    //        //        .With(x => x.Weeks = numberWeeks)
    //        //        .And(x => x.Days = numberDays)
    //        //        .Build();

    //        //    var exercises = Design.Many<Exercise>(numberExercises).Build();

    //        //    //  apply all exercises over two weeks
    //        //    for (int week = 1; week <= numberWeeks; week++)
    //        //    {
    //        //        for (int day = 1; day <= numberDays; day++)
    //        //        {
    //        //            foreach (var ex in exercises)
    //        //            {
    //        //                var re = Design.One<WorkoutRoutineExercise>()
    //        //                    .With(x => x.Exercise = ex)
    //        //                    .And(x => x.WeekId = week)
    //        //                    .And(x => x.DayId = day)
    //        //                    .And(x => x.Routine = routine)
    //        //                    .Build();

    //        //                routine.Exercises.Add(re);
    //        //            }
    //        //        }
    //        //    }

    //        //    return routine;
    //        //}

    //        //public static Plan CompletePlan(int numberOfCycles = 1, int numberWeeks = 4, int numberDays = 3, int numberExercises = 5)
    //        //{
    //        //    var routine = CompleteRoutine(numberWeeks, numberDays, numberExercises);

    //        //    var plan = new Plan(routine)
    //        //    {
    //        //        StartDate = DateTime.Now.GetNextSunday()
    //        //    };

    //        //    foreach (var cycle in plan.Cycles)
    //        //    {
    //        //        cycle.CreateExercisesForCycle();
    //        //    }

    //        //    return plan;
    //        //}
    //    }
}
