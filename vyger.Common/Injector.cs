using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using Augment;
using Augment.Caching;
using FluentValidation;
using Insight.Database;
using SimpleInjector;
using vyger.Common.Models;

namespace vyger.Common
{
    public class Injector : Container
    {
        #region Members

        //private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private Func<IPrincipal> _getPrincipal;

        #endregion

        #region Constructor

        public Injector(Func<IPrincipal> getPrincipal, ScopedLifestyle lifestyle) : base()
        {
            _getPrincipal = getPrincipal;

            Options.DefaultScopedLifestyle = lifestyle;

            RegisterAll();
        }

        #endregion

        #region UnitOfWork

        public UnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(this);
        }

        #endregion

        #region Register Mainline

        private void RegisterAll()
        {
            RegisterSupportItems();

            RegisterRepositories();

            RegisterServices();

            RegisterValidators();

            RegisterTasks();
        }

        private void RegisterSupportItems()
        {
            Register<ICacheProvider, HttpRuntimeCacheProvider>();
            Register<ICacheManager, CacheManager>();
            Register<ISecurityActor>(() => GetSecurityActor());
            Register<IDbConnection>(() => GetConnection(), Lifestyle.Scoped);
            Register<IVygerContext>(() => GetVygerContext(), Lifestyle.Scoped);
        }

        private void RegisterRepositories()
        {
            Type[] types = GetTypesOf("Repositories").ToArray();

            foreach (Type interfaceType in types.Where(x => x.IsInterface && !x.IsNested))
            {
                Register(interfaceType, () => GetRepository(interfaceType));
            }
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

        private void RegisterValidators()
        {
            Type[] types = GetTypesOf("Validators")
                .Where(x => !x.IsInterface && !x.IsNested)
                .ToArray();

            Type validatorType = typeof(IValidator<>);

            foreach (Type concreteType in types)
            {
                Type modelType = concreteType.BaseType.GetGenericArguments().First();

                Type interfaceType = validatorType.MakeGenericType(modelType);

                Register(interfaceType, concreteType);
            }
        }

        private void RegisterTasks()
        {
            Type[] types = GetTypesOf("Tasks").ToArray();

            foreach (Type concreteType in types.Where(x => !x.IsAbstract && !x.IsInterface && !x.IsNested))
            {
                Register(concreteType);
            }
        }

        private IEnumerable<Type> GetTypesOf(string nmspace)
        {
            Assembly assembly = GetType().Assembly;

            string ns = assembly.GetName().Name + "." + nmspace;

            return assembly.GetTypes().Where(x => x.Namespace == ns);
        }

        private object GetRepository(Type interfaceType)
        {
            IDbConnection conn = GetInstance<IDbConnection>();

            object repo = conn.As(interfaceType);

            return repo;
        }

        #endregion

        #region Actor/Database Support

        public ISecurityActor GetSecurityActor()
        {
            IPrincipal p = GetPrincipal();

            if (p.Identity.IsAuthenticated)
            {
                string name = p.Identity.Name;

                if (name.IsNullOrEmpty())
                {
                    name = "0";
                }

                IVygerContext db = GetInstance<IVygerContext>();

                Member member = db.Members.FirstOrDefault(x => x.MemberId.ToString() == p.Identity.Name);

                if (member != null)
                {
                    return new SecurityActor(member);
                }
            }

            return new SecurityActor(new Member());
        }

        private IPrincipal GetPrincipal()
        {
            IPrincipal p = _getPrincipal();

            if (p == null)
            {
                p = new GenericPrincipal(new GenericIdentity("0", "X"), null);
            }

            return p;
        }

        private IDbConnection GetConnection()
        {
            string environment = ConfigurationManager.AppSettings["Environment"];

            string dbkey = $"DB.{environment}";

            //logger.Debug($"Accessing Database Environment=[{environment}] DbKey=[{dbkey}]");

            ConnectionStringSettings css = ConfigurationManager.ConnectionStrings[dbkey];

            string providerName = css.ProviderName;

            DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);

            IDbConnection conn = factory.CreateConnection();

            conn.ConnectionString = css.ConnectionString;

            conn.Open();

            //logger.Debug($"Accessed Database=[{con.Database}]");

            return conn;
        }

        private IVygerContext GetVygerContext()
        {
            IDbConnection con = GetInstance<IDbConnection>();

            return new VygerContext(con);
        }

        #endregion
    }
}
