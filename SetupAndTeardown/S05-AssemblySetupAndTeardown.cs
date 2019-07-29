using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit_Demonstration.SetupAndTeardown.SetupAssembly
{
    // It's possible to do assembly-wide setup and teardown
    // which will be called once for the entire assembly.

    // Assemblies can not have multiple setup and teardown.
    // One of each method per class.
    // One class per assembly.

    // First let's declare a target field so we can sense the setup.
    public class AssemblyTargetClass
    {
        public static int SetupCounter = 0;
    }
}

// To do namespace-wide setup and teardown, we need to declare a setup class
// that is not in any namespace and mark it with the SetUpFixture attribute.

// This runs any time we run any test in the whole project.

//
// NOTE: This class has been placed in a namespace because the R# test runner
// collects up all tests that run under a setup fixture, so any test failures
// of any test will bubble up and cause this class to show up as failing,
// which is confusing.  If you want to demo assembly-wide setup, remove the
// namespace declaration from around the class below.
//
namespace NUnit_Demonstration.SetupAndTeardown.SetupAssembly
{
    [SetUpFixture]
    public class ImplementAssemblySetupAndTeardown
    {
        // Executed once before any test method in the assembly is run.
        [OneTimeSetUp]
        public void AssemblySetup()
        {
            AssemblyTargetClass.SetupCounter++;
        }

        // Executed once after all test methods in the assembly have run.
        [OneTimeTearDown]
        public void AssemblyTeardown()
        {
            AssemblyTargetClass.SetupCounter--;
        }
    }
}

namespace NUnit_Demonstration.SetupAndTeardown.SetupAssembly
{
    // Now here's an ordinary test fixture to prove it works.
    [TestFixture]
    public class AssemblySetupAndTeardown
    {
        [Test]
        public void AssemblySetupCalled()
        {
            Assert.AreEqual(1, AssemblyTargetClass.SetupCounter);
        }
    }
}
