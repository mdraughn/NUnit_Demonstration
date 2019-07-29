using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit_Demonstration.Assertions
{
    [TestFixture]
    public class RuntimeContext
    {
        [Test]
        public void CurrentContext()
        {
            // Get the current test context.
            var context = TestContext.CurrentContext;
            Assert.IsNotNull(context);
            Assert.IsInstanceOf<TestContext>(context);
        }

        [Test]
        [Author("TheAuthor")]
        [Category("TheCategory")]
        [Category("TheOtherCategory")]
        [Description("TheDescription")]
        [Property("CustomProperty1", "TheCustomProperty1")]
        [Property("CustomProperty2", "TheCustomProperty2")]
        [Property("CustomPropertyM", "Value1")]
        [Property("CustomPropertyM", "Value2")]
        [Property("CustomPropertyM", "Value3")]
        public void InformationAboutTheCurrentTest()
        {
            // You can get information about the current test.
            // Useful in helper routines that may be called from many tests.
            // Also useful in the setup and teardown.

            var context = TestContext.CurrentContext;

            // Name of the test.
            Assert.AreEqual("InformationAboutTheCurrentTest", context.Test.Name);

            // Full name of the test.
            Assert.AreEqual("NUnit_Demonstration.Assertions.RuntimeContext.InformationAboutTheCurrentTest", context.Test.FullName);

            // The properties are name-value pairs.
            // The values are all lists to allow for more than one value on properties that permit multiple values

            Assert.AreEqual(6,context.Test.Properties.Keys.Count);

            Assert.That(context.Test.Properties["Author"].First(), Is.EqualTo("TheAuthor"));

            IEnumerable<object> testProperty = context.Test.Properties["Category"];
            Assert.That(testProperty.Count, Is.EqualTo(2));
            Assert.That(testProperty.First(), Is.EqualTo("TheCategory"));
            object actual = testProperty.Skip(1).First();
            Assert.That(actual, Is.EqualTo("TheOtherCategory"));

            Assert.That(context.Test.Properties["Description"].First(), Is.EqualTo("TheDescription"));

            Assert.That(context.Test.Properties["CustomProperty1"].First(), Is.EqualTo("TheCustomProperty1"));
            Assert.That(context.Test.Properties["CustomProperty2"].First(), Is.EqualTo("TheCustomProperty2"));

            Assert.That(context.Test.Properties["CustomPropertyM"].Count, Is.EqualTo(3));
            Assert.That(context.Test.Properties["CustomPropertyM"].First(), Is.EqualTo("Value1"));
            Assert.That(context.Test.Properties["CustomPropertyM"].Skip(1).First(), Is.EqualTo("Value2"));
            Assert.That(context.Test.Properties["CustomPropertyM"].Skip(2).First(), Is.EqualTo("Value3"));
        }
        [Test]
        public void CheckTestStatus()
        {
            // You can check the status of the current test.
            // Useful in setup and teardown.

            var context = TestContext.CurrentContext;

            // The test is still running, so it's inconclusive.
            Assert.AreEqual(TestStatus.Inconclusive,context.Result.Outcome.Status);
        }

        [Test]
        public void TestDirectory()
        {
            // TestDirectory gets the directory of the current test assembly.
            var context = TestContext.CurrentContext;

            // Convert it to a URI to test it.
            var testDirectory = new Uri(context.TestDirectory);
            StringAssert.StartsWith(testDirectory.ToString(),this.GetType().Assembly.CodeBase);
        }
    }
}
