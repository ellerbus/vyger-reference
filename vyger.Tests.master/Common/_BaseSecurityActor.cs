using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vyger.Common;
using vyger.Common.Models;

namespace vyger.Tests.Common
{
    [TestClass]
    public class BaseSecurityActor
    {
        protected SecurityActor SubjectUnderTest;

        [TestInitialize]
        public void TestInitialize()
        {
            var member = Design.One<Member>().Build();

            SubjectUnderTest = new SecurityActor(member);
        }
    }
}
