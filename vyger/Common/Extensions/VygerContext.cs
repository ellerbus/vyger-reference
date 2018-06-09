using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using vyger.Common.Models;

namespace vyger.Common
{
    static class DbSetExtensions
    {
        public static IQueryable<Exercise> WithAccessFor(this IDbSet<Exercise> exercises, ISecurityActor actor)
        {
            return exercises.Where(x => x.OwnerId == -1 || x.OwnerId == actor.MemberId);
        }

        public static IQueryable<WorkoutRoutine> WithAccessFor(this IDbSet<WorkoutRoutine> routines, ISecurityActor actor)
        {
            return routines.Where(x => x.OwnerId == -1 || x.OwnerId == actor.MemberId);
        }

        public static IQueryable<WorkoutPlan> WithAccessFor(this IDbSet<WorkoutPlan> plans, ISecurityActor actor)
        {
            return plans.Where(x => x.OwnerId == actor.MemberId);
        }
    }

    public interface IVygerContext
    {
        IDbSet<Member> Members { get; }

        IDbSet<ExerciseGroup> ExerciseGroups { get; }

        IDbSet<Exercise> Exercises { get; }

        IDbSet<WorkoutRoutine> WorkoutRoutines { get; }

        IDbSet<WorkoutPlan> WorkoutPlans { get; }

        IDbSet<WorkoutLog> WorkoutLogs { get; }

        int SaveChanges();
    }

    class VygerContext : DbContext, IVygerContext
    {
        #region Constructor

        public VygerContext() : base() { }

        public VygerContext(IDbConnection con) : base(con as DbConnection, false)
        {
            Database.Log = (string x) => System.Diagnostics.Debug.Write(x);
        }

        #endregion

        #region Properties

        public IDbSet<Member> Members { get; set; }

        public IDbSet<ExerciseGroup> ExerciseGroups { get; set; }

        public IDbSet<Exercise> Exercises { get; set; }

        public IDbSet<WorkoutRoutine> WorkoutRoutines { get; set; }

        public IDbSet<WorkoutPlan> WorkoutPlans { get; set; }

        public IDbSet<WorkoutLog> WorkoutLogs { get; set; }

        #endregion

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<VygerContext>(null);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (DbEntityEntry entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.CurrentValues["CreatedAt"] = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.CurrentValues["UpdatedAt"] = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }

        #endregion
    }
}
