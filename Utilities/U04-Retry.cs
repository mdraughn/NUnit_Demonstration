using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NUnit_Demonstration.Utilities
{
    // If a test sometimes fails for transient conditions that aren't realistic,
    // you can ask that it be retried until it works.
    [TestFixture]
    public class Retry
    {
        private Random rand = new Random();

        [Test]
        [Retry(10000)]
        public void ThisTestFails99PercentOfTheTime()
        {
            if (rand.Next(100) == 0)
            {
                Assert.True(true);
            }
            else
            {
                Assert.Fail();
            }
        }
    }

    // On the other hand, if you want to want to try to catch transient problems,
    // you can ask that the test be repeated to prove its reliability.
    [TestFixture]
    [Category("ExpectedToFail")]
    public class Repeat_ExpectedToFail
    {
        private Random rand = new Random();

        [Test]
        [Repeat(10000)]
        public void ThisTestFails1PercentOfTheTime_ExpectedToFail()
        {
            if (rand.Next(100) > 0)
            {
                Assert.True(true);
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}
