using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit_Demonstration.Extension
{
    [TestFixture]
    public class ClassSetupAndTeardown
    {
        // If the setup only forms the environment for the tests -- but isn't changed
        // by them -- you can do a class-wide setup on the TestFixture class.
        // E.g. to open a database connection

        public bool FixtureSetupFlag = false;
        public int SetupCallCount = 0;

        // Executed once before any test method in the class is run.
        [OneTimeSetUp]
        public void ClassSetup()
        {
            FixtureSetupFlag = true;
            SetupCallCount++;
        }

        // Executed once after all test methods in the class have run.
        [OneTimeTearDown]
        public void ClassTeardown()
        {
            FixtureSetupFlag = false;
        }

        // The flag is set across all test methods, but setup was only called once.
        [Test]
        public void Test1()
        {
            Assert.IsTrue(FixtureSetupFlag);
            Assert.That(SetupCallCount, Is.EqualTo(1));
        }
        [Test]
        public void Test2()
        {
            Assert.IsTrue(FixtureSetupFlag);
            Assert.That(SetupCallCount, Is.EqualTo(1));
        }
        [Test]
        public void Test3()
        {
            Assert.IsTrue(FixtureSetupFlag);
            Assert.That(SetupCallCount, Is.EqualTo(1));
        }
    }

    // And, yes, the class-wide setup/teardown is also inherited.
    [TestFixture]
    public class ClassSetupAndTeardownDerived : ClassSetupAndTeardown
    {
        // If the setup only forms the environment for the tests -- but isn't changed
        // by them -- you can do a class-wide setup on the TestFixture class.

        public bool DerivedFixtureSetupFlag = false;
        public int DerivedSetupCallCount = 0;

        [OneTimeSetUp]
        public void DerivedClassWideSetup()
        {
            DerivedFixtureSetupFlag = true;
            DerivedSetupCallCount++;
        }
        [OneTimeTearDown]
        public void DerivedClassWideTeardown()
        {
            DerivedFixtureSetupFlag = false;
        }

        // The flag is set across all test methods, but setup was only called once.
        [Test]
        public void DerivedTest1()
        {
            Assert.IsTrue(FixtureSetupFlag);
            Assert.IsTrue(DerivedFixtureSetupFlag);
            Assert.That(SetupCallCount, Is.EqualTo(1));
            Assert.That(DerivedSetupCallCount, Is.EqualTo(1));
        }
        [Test]
        public void DerivedTest2()
        {
            Assert.IsTrue(FixtureSetupFlag);
            Assert.IsTrue(DerivedFixtureSetupFlag);
            Assert.That(SetupCallCount, Is.EqualTo(1));
            Assert.That(DerivedSetupCallCount, Is.EqualTo(1));
        }
        [Test]
        public void DerivedTest3()
        {
            Assert.IsTrue(FixtureSetupFlag);
            Assert.IsTrue(DerivedFixtureSetupFlag);
            Assert.That(SetupCallCount, Is.EqualTo(1));
            Assert.That(DerivedSetupCallCount, Is.EqualTo(1));
        }
    }
}
