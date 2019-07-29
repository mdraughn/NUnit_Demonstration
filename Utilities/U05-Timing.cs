using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace NUnit_Demonstration.Utilities
{
    [TestFixture]
    public class Timing
    {
        // There are two ways of handling timeout issues in NUnit.

        // If you want to avoid run-away tests that could be looping or blocked or otherwise
        // going sideways on us, use the Timeout attribute. The test will be cancelled when
        // its runtime exceeds the timeout interval.
        [Test]
        [Timeout(100)]
        [Category("ExpectedToFail")]
        public void RunsTooLongAndGetsCancelled_ExpectedToFail()
        {
            Thread.Sleep(200);
        }

        // If you're testing performance and you want the test to fail if it runs too long,
        // use the MaxTime attribute. The test will not be cancelled, it will run to completion,
        // 
        [Test]
        [MaxTime(100)]
        [Category("ExpectedToFail")]
        public void RunsTooLongAndFails_ExpectedToFail()
        {
            Thread.Sleep(200);
        }

    }
}
