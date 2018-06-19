using System.Collections.Generic;
using System.Security.Principal;
using Augment;

namespace vyger.Common
{
    #region Interface

    public interface ISecurityActor
    {
        string[] GetRoles();

        string Email { get; }

        bool IsAuthenticated { get; }
    }

    #endregion

    public class SecurityActor : ISecurityActor
    {
        #region Constructors

        public SecurityActor(string email, bool? isAuthenticated)
        {
            Email = email.AssertNotNull().ToLower();

            IsAuthenticated = isAuthenticated.GetValueOrDefault();
        }

        public SecurityActor(IIdentity identity)
            : this(identity?.Name, identity?.IsAuthenticated)
        {
            Identity = identity;
        }

        #endregion

        #region Methods

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

        public IIdentity Identity { get; private set; }

        public string Email { get; private set; }

        public bool IsAuthenticated { get; private set; }

        #endregion
    }
}