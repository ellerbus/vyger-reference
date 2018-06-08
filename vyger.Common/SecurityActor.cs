using System;
using System.Collections.Generic;
using System.Security;
using Augment;
using vyger.Common.Models;

namespace vyger.Common
{
    #region Interface

    public interface ISecurityActor
    {
        int MemberId { get; }

        bool IsAdministrator { get; }

        StatusTypes Status { get; }

        void EnsureAudit<T>(T item);

        void EnsureAudits<T>(IEnumerable<T> items);

        string[] GetRoles();

        bool Can(SecurityAccess access, Member member);
        void VerifyCan(SecurityAccess access, Member member);

        bool Can(SecurityAccess access, ExerciseGroup group);
        void VerifyCan(SecurityAccess access, ExerciseGroup group);

        bool Can(SecurityAccess access, Exercise exercise);
        void VerifyCan(SecurityAccess access, Exercise exercise);

        bool Can(SecurityAccess access, WorkoutRoutine routine);
        void VerifyCan(SecurityAccess access, WorkoutRoutine routine);

        bool Can(SecurityAccess access, WorkoutPlan plan);
        void VerifyCan(SecurityAccess access, WorkoutPlan plan);

        //bool Can(SecurityAccess access, WorkoutLog log);
        //void VerifyCan(SecurityAccess access, WorkoutLog log);
    }

    #endregion

    public class SecurityActor : ISecurityActor
    {
        #region Constructors

        public SecurityActor(Member member)
        {
            Member = member;
        }

        #endregion

        #region Methods

        public void EnsureAudits<T>(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                EnsureAudit<T>(item);
            }
        }

        public void EnsureAudit<T>(T item)
        {
            if (ReflectionHelper.HasProperty(item, "CreatedAt"))
            {
                DateTime dttm = (DateTime)ReflectionHelper.GetValueOfProperty(item, "CreatedAt");

                if (dttm == DateTime.MinValue)
                {
                    ReflectionHelper.SetValueOfProperty(item, "CreatedAt", DateTime.UtcNow);
                }
                else if (ReflectionHelper.HasProperty(item, "UpdatedAt"))
                {
                    ReflectionHelper.SetValueOfProperty(item, "UpdatedAt", DateTime.UtcNow);
                }
            }
        }

        private void VerifyWithSecurityException(bool valid, string msg)
        {
            if (!valid)
            {
                throw new SecurityException(msg);
            }
        }

        private string CreateSimpleSecurityMessage(SecurityAccess access, object item)
        {
            string msg = "'{0}' cannot {1} {2}".FormatArgs(MemberId, access, item.ToString());

            return msg;
        }

        public string[] GetRoles()
        {
            List<string> roles = new List<string>() { };

            if (Status == StatusTypes.Active)
            {
                roles.Add(Constants.Roles.ActiveMember);
            }

            if (IsAdministrator)
            {
                roles.Add(Constants.Roles.Administrator);
            }

            return roles.ToArray();
        }

        #endregion

        #region "Member" Methods

        public bool Can(SecurityAccess access, Member member)
        {
            if (access == SecurityAccess.Authenticate)
            {
                return true;
            }

            return IsAdministrator;
        }

        public void VerifyCan(SecurityAccess access, Member member)
        {
            if (member != null)
            {
                bool valid = Can(access, member);

                string msg = CreateSimpleSecurityMessage(access, member);

                VerifyWithSecurityException(valid, msg);
            }
        }

        #endregion

        #region "ExerciseGroup" Methods

        public bool Can(SecurityAccess access, ExerciseGroup group)
        {
            if (Member.IsAdministrator)
            {
                return true;
            }

            if (access == SecurityAccess.View)
            {
                return true;
            }

            return false;
        }

        public void VerifyCan(SecurityAccess access, ExerciseGroup group)
        {
            if (group != null)
            {
                bool valid = Can(access, group);

                string msg = CreateSimpleSecurityMessage(access, group);

                VerifyWithSecurityException(valid, msg);
            }
        }

        #endregion

        #region "Exercise" Methods

        public bool Can(SecurityAccess access, Exercise exercise)
        {
            if (Member.IsAdministrator)
            {
                return true;
            }

            if (access == SecurityAccess.Update)
            {
                return exercise.OwnerId == Member.MemberId;
            }

            if (access == SecurityAccess.View)
            {
                return exercise.OwnerId.IsIn(-1, Member.MemberId);
            }

            return false;
        }

        public void VerifyCan(SecurityAccess access, Exercise exercise)
        {
            if (exercise != null)
            {
                bool valid = Can(access, exercise);

                string msg = CreateSimpleSecurityMessage(access, exercise);

                VerifyWithSecurityException(valid, msg);
            }
        }

        #endregion

        #region "Routine" Methods

        public bool Can(SecurityAccess access, WorkoutRoutine routine)
        {
            if (Member.IsAdministrator)
            {
                return true;
            }

            if (access == SecurityAccess.Create)
            {
                return true;
            }

            if (access == SecurityAccess.Update)
            {
                return routine.OwnerId == Member.MemberId;
            }

            if (access == SecurityAccess.View)
            {
                return routine.OwnerId.IsIn(-1, Member.MemberId);
            }

            return false;
        }

        public void VerifyCan(SecurityAccess access, WorkoutRoutine routine)
        {
            if (routine != null)
            {
                bool valid = Can(access, routine);

                string msg = CreateSimpleSecurityMessage(access, routine);

                VerifyWithSecurityException(valid, msg);
            }
        }

        #endregion

        #region "Plan" Methods

        public bool Can(SecurityAccess access, WorkoutPlan plan)
        {
            if (Member.IsAdministrator)
            {
                return true;
            }

            if (access == SecurityAccess.Create)
            {
                return true;
            }

            if (access == SecurityAccess.Update)
            {
                return plan.OwnerId == Member.MemberId;
            }

            if (access == SecurityAccess.View)
            {
                return plan.OwnerId.IsIn(-1, Member.MemberId);
            }

            return false;
        }

        public void VerifyCan(SecurityAccess access, WorkoutPlan plan)
        {
            if (plan != null)
            {
                bool valid = Can(access, plan);

                string msg = CreateSimpleSecurityMessage(access, plan);

                VerifyWithSecurityException(valid, msg);
            }
        }

        #endregion

        #region Properties

        public Member Member { get; private set; }

        public int MemberId { get { return Member == null ? 0 : Member.MemberId; } }

        public bool IsAdministrator { get { return Member == null ? false : Member.IsAdministrator; } }

        public StatusTypes Status { get { return Member == null ? StatusTypes.None : Member.Status; } }

        #endregion
    }
}
