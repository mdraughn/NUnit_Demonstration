using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NUnit_Demonstration.SetupAndTeardown
{
    [TestFixture]
    public class Culture
    {
        // Tests normally run under the ambient culture.
        [Test]
        public void NumberFormatDefault()
        {
            Assert.AreEqual("$5,000.10", 5000.1.ToString("C"));
        }

        // You can filter tests to only run under a specific ambient culture.
        [Test]
        [Culture("en")]
        public void TestSomethingCultureEn_WillRun()
        {
            Assert.AreEqual("$5,000.10", 5000.1.ToString("C"));
        }

        [Test]
        [Culture("de")]
        public void TestSomethingCultureDe_WillBeIgnored()
        {
            Assert.AreEqual("5.000,10 €", 5000.1.ToString("C"));
        }

        // You can also specify that a test executes under a specific culture.
        [Test]
        [SetCulture("de")]
        public void NumberFormatGerman()
        {
            Assert.AreEqual("5.000,10 €", 5000.1.ToString("C"));
        }

    }
}
