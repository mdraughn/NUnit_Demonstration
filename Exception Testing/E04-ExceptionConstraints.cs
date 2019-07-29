using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;

namespace NUnit_Demonstration.Exception_Testing
{
    [TestFixture]
    public partial class ExceptionConstraints
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

        // Like everything else, exceptions catching has a constraint version.
        [Test]
        public void ThrowsConstraintAnyException()
        {
            // It's a little weird, because the first part -- normally the "actual" value --
            // has to be a delegate for the code to be checked.
            Assert.That(() => ValidateMatter("foo"), Throws.Exception);
        }

        // You don't need to assert if you expect it not to throw an exception.
        [Test]
        public void NoException()
        {
            ValidateMatter("aaa");
        }

        // But you can make it more explicit that that's what you are checking for
        // by asserting that it throws nothing.
        [Test]
        public void ThrowsNothing()
        {
            Assert.That(() => ValidateMatter("aaa"), Throws.Nothing);
        }

        // You can specify the expected exception type in several ways.
        [Test]
        public void ThrowsConstraintType()
        {
            // As a chained constraint on the Exception constraint.
            Assert.That(() => ValidateMatter("foo"), Throws.Exception.TypeOf(typeof(ArgumentException)));
            Assert.That(() => ValidateMatter("foo"), Throws.Exception.TypeOf<ArgumentException>());

            // Or just leave out the Exception keyword and specify the type of thing to be thrown.
            Assert.That(() => ValidateMatter("foo"), Throws.TypeOf(typeof(ArgumentException)));
            Assert.That(() => ValidateMatter("foo"), Throws.TypeOf<ArgumentException>());
        }

        // Or, if it's a well-known exception, you can use a predefined name.
        [Test]
        public void ThrowsConstraintWellKnown()
        {
            Assert.That(() => ValidateMatter("foo"), Throws.ArgumentException);

            // Also works for these two types.
            Assert.That(() => { throw new InvalidOperationException(); }, Throws.InvalidOperationException);
            Assert.That(() => { throw new TargetInvocationException(new ArithmeticException()); },
                Throws.TargetInvocationException);
        }

        // That last one was weird because it required an inner exception.
        [Test]
        public void ThrowsConstraintInnerException()
        {
            // We could check that too with a chained exception.
            Assert.That(() => { throw new TargetInvocationException(new ArithmeticException()); },
                Throws.TargetInvocationException.With.InnerException.TypeOf<ArithmeticException>());
        }

        // A similar chaining operation can be used to constrain the message.
        [Test]
        public void ThrowsConstraintCheckMessage()
        {
            Assert.That(() => ValidateMatter("foo"),
                Throws.ArgumentException
                    .With.Message.StartsWith("matter not found")
                    .And.Message.Contains("foo"));
        }
    }

    [TestFixture]
    public partial class ExceptionConstraints_ExpectedToFail
    {
        // The Throws constraint will NOT work with derived types.
        [Test]
        [Category("ExpectedToFail")]
        public void ThrowsDerived_ExpectedToFail()
        {
            Assert.That(() => ExceptionConstraints.ValidateMatter("foo"), Throws.TypeOf<Exception>());
        }
    }
    [TestFixture]
    public partial class ExceptionConstraints
    {
        // But you can catch an instance of it.
        [Test]
        public void ThrowsDerivedInstanceOf()
        {
            Assert.That(() => ValidateMatter("foo"), Throws.Exception.InstanceOf<Exception>());

            // Which has a shortcut notation.
            Assert.That(() => ValidateMatter("foo"), Throws.InstanceOf<Exception>());
        }

        // Oddly, the Throws() assertion can take a constraint.
        [Test]
        public void ThrowsAssertionTakesConstraint()
        {
            Assert.Throws(Is.InstanceOf<Exception>(), () => ValidateMatter("foo"));
        }

    }
}
