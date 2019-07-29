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
//    // You can specify a global exception handler for tests by implementing IExpectException.
//    [TestFixture]
//    public partial class ExceptionHandling : IExpectException
//    {
//        public void HandleException(Exception ex)
//        {
//            Assert.IsInstanceOf(typeof(ArgumentException), ex);
//            StringAssert.Contains("foo", ex.Message);
//        }

//        // Some test data.
//        private static string[] knownMatters = {"aaa", "bbb", "ddd", "fff"};

//        // Checks the list and throws a message that includes the matter name argument
//        private static void ValidateMatter(string matterName)
//        {
//            if (!knownMatters.Contains(matterName))
//            {
//                throw new ArgumentException(String.Format("matter not found: {0}", matterName));
//            }
//        }

//        // Now just specify ExpectedException and the handler will be used.
//        [Test, ExpectedException]
//        public void ExpectedExceptionDefaultHandler()
//        {
//            ValidateMatter("foo");
//        }
//    }

//    [TestFixture]
//    public class ExceptionHandling_ExpectedToFail : IExpectException
//    {
//        public void HandleException(Exception ex)
//        {
//            Assert.IsInstanceOf(typeof(ArgumentException), ex);
//            StringAssert.Contains("foo", ex.Message);
//        }

//        // If there's no exception, it will fail.
//        [Test, ExpectedException]
//        [Category("ExpectedToFail")]
//        public void NoExceptionForDefaultHandler_ExpectedToFail()
//        {
//        }
//    }
//    [TestFixture]
//    public partial class ExceptionHandling : IExpectException
//    {

//        // You can also specify a handler explicitly
//        public void AlternateExceptionHandler(Exception ex)
//        {
//            StringAssert.Contains("bar", ex.Message);
//        }

//        // Now just specify ExpectedException and the handler will be used.
//        [Test, ExpectedException(Handler = "AlternateExceptionHandler")]
//        public void ExpectedExceptionExplicitHandler()
//        {
//            ValidateMatter("bar");
//        }

//    }
//}
