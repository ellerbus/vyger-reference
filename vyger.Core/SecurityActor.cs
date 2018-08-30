using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Augment;

namespace vyger.Core
{
    public interface ISecurityActor : IPrincipal
    {
        string[] GetRoles();

        string Email { get; }

        bool IsAuthenticated { get; }

        string AuthenticationToken { get; }
    }

    class SecurityIdentity : IIdentity
    {
        public string Name { get; set; }

        public string AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }
    }

    public class SecurityActor : ISecurityActor
    {
        #region Constructors

        public SecurityActor(string email, bool? isAuthenticated, string token)
        {
            Email = email.AssertNotNull().ToLower();

            IsAuthenticated = isAuthenticated.GetValueOrDefault();

            AuthenticationToken = token;

            Identity = new SecurityIdentity()
            {
                Name = email,
                AuthenticationType = "vyger",
                IsAuthenticated = isAuthenticated.GetValueOrDefault()
            };
        }

        #endregion

        #region Methods

        public string[] GetRoles()
        {
            List<string> roles = new List<string>() { };

            if (Email.IsNotEmpty())
            {
                roles.Add(Constants.Roles.ActiveMember);
                roles.Add("Token:" + AuthenticationToken);
            }

            return roles.ToArray();
        }

        public bool IsInRole(string role)
        {
            return GetRoles().Any(x => x == role);
        }

        #endregion

        #region Properties

        public IIdentity Identity { get; private set; }

        public string Email { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public string AuthenticationToken { get; private set; }

        #endregion
    }
}