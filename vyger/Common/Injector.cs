using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using Augment;
using Augment.Caching;
using SimpleInjector;
using vyger.Models;
using vyger.Services;

namespace vyger.Common
{
    public class Injector : Container
    {
        #region Constructor

        public Injector(ScopedLifestyle lifestyle) : base()
        {
            Options.DefaultScopedLifestyle = lifestyle;

            RegisterAll();
        }

        #endregion

        #region Register Mainline

        private void RegisterAll()
        {
            RegisterSupportItems();

            RegisterServices();
        }

        private void RegisterSupportItems()
        {
            Register<ICacheProvider, HttpRuntimeCacheProvider>();
            Register<ICacheManager, CacheManager>();
            Register<ISecurityActor>(() => GetSecurityActor());
        }

        private void RegisterServices()
        {
            Type[] types = GetTypesOf("Services").ToArray();

            foreach (Type interfaceType in types.Where(x => x.IsInterface && !x.IsNested))
            {
                foreach (Type concreteType in types.Where(x => !x.IsInterface))
                {
                    if (interfaceType.IsAssignableFrom(concreteType))
                    {
                        Register(interfaceType, concreteType);
                        break;
                    }
                }
            }
        }

        private IEnumerable<Type> GetTypesOf(string nmspace)
        {
            Assembly assembly = GetType().Assembly;

            string ns = assembly.GetName().Name + "." + nmspace;

            return assembly.GetTypes().Where(x => x.Namespace == ns);
        }

        #endregion

        #region Actor Support

        public ISecurityActor GetSecurityActor()
        {
            IPrincipal p = GetPrincipal();

            if (p != null && p.Identity != null & p.Identity.IsAuthenticated)
            {
                string name = p.Identity.Name;

                if (name.IsNullOrEmpty())
                {
                    name = "x";
                }

                ISecurityActor sa = new SecurityActor(new Member(p.Identity.Name));

                IMemberService members = new MemberService(sa);

                Member member = members.GetMember();

                if (member != null)
                {
                    return new SecurityActor(member);
                }
            }

            return new SecurityActor(new Member());
        }

        private static IPrincipal GetPrincipal()
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                IPrincipal p = HttpContext.Current.User;

                if (p.Identity != null)
                {
                    return p;
                }
            }

            return null;
        }

        #endregion
    }
}
