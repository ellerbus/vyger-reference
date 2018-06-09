using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using vyger.Common.Models;
using vyger.Common.Repositories;

namespace vyger.Common
{
    public static class DbContextExtensions
    {
        #region Misc Methods

        public static void EnsureMigrationsApplied(this DbContext db)
        {
            if (!db.Database.GetAppliedMigrations().Any())
            {
                db.Database.Migrate();
            }
        }

        #endregion

        #region Seeding

        public static void EnsureSeeded(this ApplicationDatabaseContext db)
        {
            SeedExercises(db);
            SeedRoutines(db);
            SeedPlans(db);
        }

        private static void SeedExercises(ApplicationDatabaseContext db)
        {
            if (!db.Exercises.Any())
            {
                string json = ReadEmbeddedResource("Exercises");

                IList<Exercise> exercises = JsonConvert.DeserializeObject<IList<Exercise>>(json);

                db.AddRange(exercises);
                db.SaveChanges();
            }
        }

        private static void SeedRoutines(ApplicationDatabaseContext db)
        {
            if (!db.Routines.Any())
            {
                string json = ReadEmbeddedResource("Routines");

                IDictionary<string, Exercise> exercises = db.Exercises.AsNoTracking().ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

                IList<Routine> routines = JsonConvert.DeserializeObject<IList<Routine>>(json);

                IEnumerable<RoutineExercise> routineExercises = routines
                    .Where(x => x.Exercises != null)
                    .SelectMany(x => x.Exercises);

                foreach (Routine routine in routines)
                {
                    foreach (RoutineExercise re in routine.Exercises)
                    {
                        Exercise ex = null;

                        if (!exercises.TryGetValue(re.Exercise.Name, out ex))
                        {
                            throw new InvalidDataException($"Unknown Exercise: {re.Exercise.Name}");
                        }

                        re.Routine = routine;
                        re.Exercise = ex;
                    }
                }

                db.Routines.AttachRange(routines);
                db.SaveChanges();
            }
        }

        private static void SeedPlans(ApplicationDatabaseContext db)
        {
            if (!db.Plans.Any())
            {
                string json = ReadEmbeddedResource("Plans");

                IDictionary<string, Exercise> exercises = db.Exercises.ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);
                IDictionary<string, Routine> routines = db.Routines.ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

                IList<Plan> plans = JsonConvert.DeserializeObject<IList<Plan>>(json);

                foreach (Plan plan in plans)
                {
                    Routine r = null;

                    if (!routines.TryGetValue(plan.Routine.Name, out r))
                    {
                        throw new InvalidDataException($"Unknown Routine: {plan.Routine.Name}");
                    }

                    plan.Routine = r;

                    if (plan.Cycles != null)
                    {
                        foreach (PlanCycle pc in plan.Cycles)
                        {
                            Exercise ex = null;

                            if (!exercises.TryGetValue(pc.Exercise.Name, out ex))
                            {
                                throw new InvalidDataException($"Unknown Exercise: {pc.Exercise.Name}");
                            }

                            pc.Exercise = ex;
                        }
                    }
                }

                db.Plans.AddRange(plans);
                db.SaveChanges();
            }
        }

        private static string ReadEmbeddedResource(string name)
        {
            Assembly assembly = typeof(DbContextExtensions).Assembly;

            using (Stream s = assembly.GetManifestResourceStream($"vyger.Common.Models.Seeds.{name}.json"))
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        #endregion
    }
}
