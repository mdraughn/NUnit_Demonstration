using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit_Demonstration.Extension
{
    // Just as there can now be multiple setup/teardown methods for a test class,
    // they can also be inherited.  In this case, base setup will happen before
    // derived setup, and base teardown will happen after derived teardown.

    [TestFixture]
    public class InheritedSetupAndTeardown_Base
    {
        // Counter set by base class only.
        public int BaseCounter = 0;

        // Counter shared by base and derived classes.
        public int SharedCounter = 0;

        [SetUp]
        public void BaseSetup()
        {
            BaseCounter++;
            SharedCounter++;
        }

        [TearDown]
        public void BaseTeardown()
        {
            BaseCounter--;
            SharedCounter--;
        }

        // Verify that the base setup was called.
        [Test]
        public void VerifyBaseCounter()
        {
            Assert.AreEqual(1,BaseCounter);
        }

        // Note that all the base test methods are called on derived classes.
        // Consequently, if a base field or property can be modified by derived
        // classes, it is not possible in principle to predict its value when used
        // from the base class.
        //
        // In this case, we happen to know that the derived class increments the
        // shared counter, so we can test for that case.
        [Test]
        public void VerifySharedCounterFromBase()
        {
            if(this.GetType() == typeof(InheritedSetupAndTeardown_Derived)) {
                Assert.AreEqual(2, SharedCounter);
            } else {
                Assert.AreEqual(1, SharedCounter);
            }
        }
    }

    [TestFixture]
    public class InheritedSetupAndTeardown_Derived : InheritedSetupAndTeardown_Base
    {
        public int DerivedCounter = 0;

        [SetUp]
        public void DerivedSetup()
        {
            Assert.That(BaseCounter, Is.EqualTo(1));
            DerivedCounter++;
            SharedCounter++;
        }

        [TearDown]
        public void DerivedTeardown()
        {
            Assert.That(BaseCounter, Is.EqualTo(1));
            DerivedCounter--;
            SharedCounter--;
        }

        // Verify that the base and derived setup were called.
        [Test]
        public void VerifyDerivedCounter()
        {
            Assert.AreEqual(1, BaseCounter);
            Assert.AreEqual(1, DerivedCounter);
        }

        // The shared counter has been incremented twice.
        [Test]
        public void VerifySharedCounterFromDerived()
        {
            Assert.AreEqual(2, SharedCounter);
        }
    }
}
