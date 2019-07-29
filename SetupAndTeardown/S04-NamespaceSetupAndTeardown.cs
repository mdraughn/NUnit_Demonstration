using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit_Demonstration.SetupAndTeardown.SetupNamespace
{
    // It's possible to do namespace-wide setup and teardown
    // which will be called once for all classes in the namespace.

    // Namespaces can not have multiple setup and teardown.
    // One of each method per class.
    // One class per namespace.

    // First let's declare a target field so we can sense the setup.
    public class NamespaceTargetClass
    {
        public static int SetupCounter = 0;
    }

    // To do namespace-wide setup and teardown, we need to declare a setup class
    // the will do the work.
    
    [SetUpFixture]
    public class NamespaceSetupAndTeardown
    {
        // Executed once before any test method in the namespace is run.
        [OneTimeSetUp]
        public void NamespaceSetup()
        {
            NamespaceTargetClass.SetupCounter++;
        }

        // Executed once after all test methods in the namespace have run.
        [OneTimeTearDown]
        public void NamespaceTeardown()
        {
            NamespaceTargetClass.SetupCounter++;
        }
    }

    // Now here's an ordinary test fixture to prove it works.
    [TestFixture]
    public class NamespaceSetupAndTeardownTest
    {
        [Test]
        public void NamespaceSetupCalled()
        {
            Assert.AreEqual(1, NamespaceTargetClass.SetupCounter);
        }
    }

}

// Setup is base on the test, not the things being tested,
// so setup does not affect tests called outside the namespace.
namespace NUnit_Demonstration.SetupAndTeardown.NonSetupNamespace
{
    // Now here's an ordinary test fixture to prove it works.
    [TestFixture]
    public class NamespaceSetupAndTeardownNotDone
    {
        [Test]
        public void NamespaceSetupNotCalledFromOtherNamespaces()
        {
            Assert.AreEqual(0, NUnit_Demonstration.SetupAndTeardown.SetupNamespace.NamespaceTargetClass.SetupCounter);
        }
    }
}
