using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests
{
    [TestFixture]
    public abstract class TestBase 
    {
        [SetUp]
        public async Task RunBeforeEachTest()
        {
            await Testing.ResetAsync();
        }
    }
}
