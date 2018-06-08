using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using Moq.Language.Flow;
using vyger.Common;

namespace vyger.Tests
{
    static class MoqExtensions
    {
        public static IReturnsResult<IVygerContext> Returns<T>(this ISetup<IVygerContext, IDbSet<T>> setup, IList<T> table) where T : class
        {
            var dbSet = new Mock<IDbSet<T>>();

            dbSet.As<IQueryable<T>>().Setup(q => q.Provider).Returns(() => table.AsQueryable().Provider);
            dbSet.As<IQueryable<T>>().Setup(q => q.Expression).Returns(() => table.AsQueryable().Expression);
            dbSet.As<IQueryable<T>>().Setup(q => q.ElementType).Returns(() => table.AsQueryable().ElementType);
            dbSet.As<IQueryable<T>>().Setup(q => q.GetEnumerator()).Returns(() => table.AsQueryable().GetEnumerator());

            return setup.Returns(dbSet.Object);
        }

        public static IQueryable<T> Returns<T>(this Mock<IQueryable<T>> mockQueryable, IList<T> table) where T : class
        {
            mockQueryable.Setup(q => q.Provider).Returns(() => table.AsQueryable().Provider);
            mockQueryable.Setup(q => q.Expression).Returns(() => table.AsQueryable().Expression);
            //mockQueryable.Setup(q => q.ElementType).Returns(() => table.AsQueryable().ElementType);
            //mockQueryable.Setup(q => q.GetEnumerator()).Returns(() => table.AsQueryable().GetEnumerator());

            return mockQueryable.Object;
        }
    }
}
