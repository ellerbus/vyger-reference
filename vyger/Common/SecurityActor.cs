using System.Collections.Generic;
using Augment;

namespace vyger.Common
{
    #region Interface

    public interface ISecurityActor
    {
        string[] GetRoles();

        string Email { get; }
    }

    #endregion

    public class SecurityActor : ISecurityActor
    {
        #region Constructors

        public SecurityActor(string email)
        {
            Email = email.AssertNotNull().ToLower();
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

        public string Email { get; private set; }

        #endregion
    }
}