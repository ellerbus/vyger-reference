using System;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace vyger.Common
{
    public class UnitOfWork : IDisposable
    {
        #region Members

        private Scope _scope;

        #endregion

        #region Constructors

        public UnitOfWork(Injector container)
        {
            _scope = AsyncScopedLifestyle.BeginScope(container);
        }

        #endregion

        #region Methods

        public T GetInstance<T>() where T : class
        {
            return _scope.Container.GetInstance<T>();
        }

        public void Dispose()
        {
            if (_scope != null)
            {
                _scope.Dispose();
            }

            _scope = null;
        }

        #endregion
    }
}
