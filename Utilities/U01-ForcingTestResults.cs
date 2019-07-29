using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace NUnit_Demonstration.Utilities
{
    [TestFixture]
    public partial class ForcingTestResults
    {
        [Test]
        public void ForcePass()
        {
            // Pass() can be used to pass a test with a message, which may be formatted.
            Assert.Pass("Did not test {0} boundary cases.", 3);
        }

        [Test]
        public void ForcePassImmediate()
        {
            // Pass ends the test immediately via an exception, ignoring any other tests.
            Assert.Pass();

            // Never reaches this one.
            Assert.AreEqual(5, 2 + 2);
        }
    }

    [TestFixture]
    public class ForcingTestResults_ExpectedToFail
    {
        [Test]
        [Category("ExpectedToFail")]
        public void ForceFail_ExpectedToFail()
        {
            // Fail() can be used to fail a test with a message, which can be formatted.
            Assert.Fail("Bad stuff in item {0}", 9);
        }

        public static void AssertIsValidTaskCodeFormat(string taskCode)
        {
            var regex = new Regex("^[a-zA-Z]{3}[1-9][0-9]{2}$");
            if (!regex.IsMatch(taskCode))
            {
                Assert.Fail("Invalid task code format: {0}", taskCode);
            }
        }

        [Test]
        [Category("ExpectedToFail")]
        public void ForceFailAssertion_ExpectedToFail()
        {
            // Fail() can be used to implement your own assertions.
            AssertIsValidTaskCodeFormat("AB100");
            AssertIsValidTaskCodeFormat("Z000");
        }
    }
    [TestFixture]
    public partial class ForcingTestResults
    {

        [Test]
        public void ForceInclusive()
        {
            // Report as inconclusive.
            // Usually for tests which could not be completed for reasons that
            // do not constitute a regression.
            Assert.Inconclusive();
        }

        [Test]
        public void ForceIgnore()
        {
            // Report as if it were ignored. Not much use for this.
            Assert.Ignore();
        }

    }
}
