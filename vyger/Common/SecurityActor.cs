using System;
using System.Collections.Generic;
using Augment;
using vyger.Models;

namespace vyger.Common
{
    #region Interface

    public interface ISecurityActor
    {
        void EnsureAudit<T>(T item);

        void EnsureAudits<T>(IEnumerable<T> items);

        string[] GetRoles();

        string Email { get; }
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

        public string[] GetRoles()
        {
            List<string> roles = new List<string>() { };

            if (Email.IsNotEmpty())
            {
                roles.Add(Constants.Roles.ActiveMember);
            }

            return roles.ToArray();
        }

        #endregion

        #region Properties

        public Member Member { get; private set; }

        public string Email { get { return Member == null ? null : Member.Email; } }

        #endregion
    }
}
