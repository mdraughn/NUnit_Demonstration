using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit_Demonstration.Extension
{
    [TestFixture]
    public class TestSetupAndTeardown
    {
        public bool PerTestSetupFlag = false;

        public int CounterA = 0;
        public int CounterB = 0;

        // Setup and TearDown mark methods that will bracket every test method.

        // This will be called before each test.
        [SetUp]
        public void PerTestSetup()
        {
            PerTestSetupFlag = true;
        }

        // This will be called after each test.
        [TearDown]
        public void PerTestTeardown()
        {
            PerTestSetupFlag = false;
            CounterA = 0;
            CounterB = 0;
        }

        [Test]
        public void SetupIsDone()
        {
            Assert.IsTrue(PerTestSetupFlag);
        }

        // These next two would not both pass if the counters weren't being reset
        // between calls -- one would have a lingering value.
        [Test]
        public void CheckCountersA()
        {
            CounterA++;
            Assert.That(CounterA, Is.EqualTo(1));
            Assert.That(CounterB, Is.EqualTo(0));
        }
        [Test]
        public void CheckCountersB()
        {
            CounterB++;
            Assert.That(CounterA, Is.EqualTo(0));
            Assert.That(CounterB, Is.EqualTo(1));
        }

        // You can define multiple setup and teardown methods per class,
        // although there is no guarantee of order.
        public bool SecondPerTestSetupFlag = false;

        public int CounterC = 0;
        public int CounterD = 0;

        // This will be called before each test.
        [SetUp]
        public void SecondPerTestSetup()
        {
            SecondPerTestSetupFlag = true;
        }

        // This will be called after each test.
        [TearDown]
        public void SecondPerTestTeardown()
        {
            SecondPerTestSetupFlag = false;
            CounterC = 0;
            CounterD = 0;
        }

        // Those get run too.
        [Test]
        public void SecondSetupIsDone()
        {
            Assert.IsTrue(SecondPerTestSetupFlag);
        }
        [Test]
        public void CheckCountersC()
        {
            CounterC++;
            Assert.That(CounterC, Is.EqualTo(1));
            Assert.That(CounterD, Is.EqualTo(0));
        }
        [Test]
        public void CheckCountersD()
        {
            CounterD++;
            Assert.That(CounterC, Is.EqualTo(0));
            Assert.That(CounterD, Is.EqualTo(1));
        }

        // Setup and teardown can also be static.
        public static bool StaticPerTestSetupFlag = false;
        static public int CounterE = 0;
        static public int CounterF = 0;

        [SetUp]
        public static void StaticPerTestSetup()
        {
            StaticPerTestSetupFlag = true;
        }

        // This will be called after each test.
        [TearDown]
        public static void StaticPerTestTeardown()
        {
            StaticPerTestSetupFlag = false;
            CounterE = 0;
            CounterF = 0;
        }

        // Static setups run too.
        [Test]
        public void StaticSetupIsDone()
        {
            Assert.IsTrue(SecondPerTestSetupFlag);
        }
        [Test]
        public void CheckCountersE()
        {
            CounterE++;
            Assert.That(CounterE, Is.EqualTo(1));
            Assert.That(CounterF, Is.EqualTo(0));
        }
        [Test]
        public void CheckCountersF()
        {
            CounterF++;
            Assert.That(CounterE, Is.EqualTo(0));
            Assert.That(CounterF, Is.EqualTo(1));
        }

        // In fact, every setup and teardown routine we've defined will run.
        [Test]
        public void AllSetupsRun()
        {
            Assert.IsTrue(StaticPerTestSetupFlag);
            Assert.IsTrue(PerTestSetupFlag);
            Assert.IsTrue(SecondPerTestSetupFlag);
            Assert.That(CounterA, Is.EqualTo(0));
            Assert.That(CounterB, Is.EqualTo(0));
            Assert.That(CounterC, Is.EqualTo(0));
            Assert.That(CounterD, Is.EqualTo(0));
            Assert.That(CounterE, Is.EqualTo(0));
            Assert.That(CounterF, Is.EqualTo(0));
        }

    }
}
