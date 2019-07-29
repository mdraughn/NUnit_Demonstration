using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit_Demonstration.Exception_Testing
{
    [TestFixture]
    public partial class ThrowsAsserts
    {
        // Some test data.
        private static string[] knownMatters = {"aaa", "bbb", "ddd", "fff"};

        // Checks the list and throws a message
        public static void ValidateMatter(string matterName)
        {
            if (!knownMatters.Contains(matterName))
            {
                throw new ArgumentException(String.Format("matter not found: {0}", matterName));
            }
        }

        //
        // NUnit 3.0 stopped supporting the ExpectedException attribute.
        //

        // ExpectedException only allows one exception per test.

        //[Test, ExpectedException(typeof(ArgumentException),
        //    MatchType = MessageMatch.StartsWith, ExpectedMessage = "matter not found")]
        //public void ExpectedExceptionNeverTested()
        //{
        //    // If this test works and throws an exception...
        //    ValidateMatter("foo");

        //    // ... then this test is never even run.
        //    ValidateMatter("aaa");
        //}

        //[Test, ExpectedException(typeof(ArgumentException),
        //    MatchType = MessageMatch.StartsWith, ExpectedMessage = "matter not found")]
        //public void ExpectedExceptionFailureHidden()
        //{
        //    // If this test fails to throw an exception...
        //    ValidateMatter("aaa");

        //    // ...this test could succeed and the thrown exception will mask the failure.
        //    ValidateMatter("bar");
        //}

        // The solution is to use the Throws assertion, which takes the code to be run as
        // a delegate.
        [Test]
        public void ThrowsTwo()
        {
            // The delegate to run can be a lambda expression.
            Assert.Throws<ArgumentException>(() => ValidateMatter("foo"));

            // Or it can be a more complex bit of code.
            Assert.Throws<ArgumentException>(() =>
            {
                string s1 = "b";
                string s2 = "a";
                string matterName = (s1 + s2 + "r");
                ValidateMatter(matterName);
            });
        }

        // You can also check does not throw.
        [Test]
        public void DoesNotThrow()
        {
            Assert.DoesNotThrow(() => ValidateMatter("aaa"));
            Assert.DoesNotThrow(() =>
            {
                string s1 = "b";
                string s2 = "b";
                string matterName = (s1 + s2 + "b");
                ValidateMatter(matterName);
            });
        }

        // This solves both ExpectedException cases above.
        [Test]
        public void MixedExpectations()
        {
            Assert.Throws<ArgumentException>(() => ValidateMatter("foo"));
            Assert.DoesNotThrow(() => ValidateMatter("aaa"));

            Assert.DoesNotThrow(() => ValidateMatter("aaa"));
            Assert.Throws<ArgumentException>(() => ValidateMatter("bar"));
        }

        [Test]
        public void ThrowsCheckingMessage()
        {
            // Throws<>() returns the exception, so you can check it with additional assertions.
            ArgumentException ex = Assert.Throws<ArgumentException>(() => ValidateMatter("foo"));
            StringAssert.Contains("foo", ex.Message);
        }
    }

    [TestFixture]
    public partial class ThrowsAsserts_ExpectedToFail
    {
        // Throws<>() will NOT work with derived types.
        [Test]
        [Category("ExpectedToFail")]
        public void ThrowsDerived_ExpectedToFail()
        {
            Assert.Throws<Exception>(() => ThrowsAsserts.ValidateMatter("foo"));
        }
    }
    [TestFixture]
    public partial class ThrowsAsserts
    {
        // But Catch<>() will.
        [Test]
        public void CatchDerived()
        {
            Assert.Catch<Exception>(() => ValidateMatter("foo"));
        }

    }
}
