//
// NUnit 3.0 stopped supporting the ExpectedException attribute.
//
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using NUnit.Framework;

//namespace NUnit_Demonstration.Exception_Testing
//{
//    [TestFixture]
//    public partial class ExpectationAttribute
//    {
//        // Some test data.
//        private static string[] knownMatters = {"aaa", "bbb", "ddd", "fff"};

//        // Checks the list and throws a message
//        public static void ValidateMatter(string matterName)
//        {
//            if (!knownMatters.Contains(matterName))
//            {
//                throw new ArgumentException(String.Format("matter not found: {0}", matterName));
//            }
//        }

//        // You can always expect an exception.
//        [Test, ExpectedException(typeof(ArgumentException))]
//        public void ExpectedException()
//        {
//            ValidateMatter("foo");
//        }
//    }

//    [TestFixture]
//    public class ExpectationAttribute_ExpectedToFail
//    {
//        // This will NOT work with derived types.
//        [Test, ExpectedException(typeof(Exception))]
//        [Category("ExpectedToFail")]
//        public void ExpectedExceptionDerived_ExpectedToFail()
//        {
//            ExpectationAttribute.ValidateMatter("foo");
//        }
//    }
//    [TestFixture]
//    public partial class ExpectationAttribute
//    {
//        // You can look at the message.
//        [Test, ExpectedException(typeof(ArgumentException), ExpectedMessage = "matter not found: foo")]
//        public void ExpectedExceptionMessage()
//        {
//            ValidateMatter("foo");
//        }

//        // You can also specify match types for the message

//        // Match the expected messaged exactly (the default)
//        [Test, ExpectedException(typeof(ArgumentException),
//            MatchType = MessageMatch.Exact, ExpectedMessage = "matter not found: foo")]
//        public void ExpectedExceptionMessageExact()
//        {
//            ValidateMatter("foo");
//        }

//        // Match the start of the expected message
//        [Test, ExpectedException(typeof(ArgumentException),
//            MatchType = MessageMatch.StartsWith, ExpectedMessage = "matter not found")]
//        public void ExpectedExceptionMessageStart()
//        {
//            ValidateMatter("foo");
//        }

//        // Expect the message to contain a string
//        [Test, ExpectedException(typeof(ArgumentException),
//            MatchType = MessageMatch.Contains, ExpectedMessage = "foo")]
//        public void ExpectedExceptionMessageContains()
//        {
//            ValidateMatter("foo");
//        }

//        // Match a regular expression
//        [Test, ExpectedException(typeof(ArgumentException),
//            MatchType = MessageMatch.Regex, ExpectedMessage = "^matter not found: .*$")]
//        public void ExpectedExceptionMessageRegex()
//        {
//            ValidateMatter("foo");
//        }

//    }
//}
