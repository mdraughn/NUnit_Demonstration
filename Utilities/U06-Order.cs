using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NUnit_Demonstration.Utilities
{
    // Without ordering, you can't be sure of the relationship between tests.

    [TestFixture]
    public class Order_ExpectedToFail
    {
        private int count;

        [OneTimeSetUp]
        public void SetUp()
        {
            count = 0;
        }

        // Ordering 

        [Test]
        public void C_Step1()
        {
            Assert.That(count, Is.EqualTo(0));
            count++;
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        [Category("ExpectedToFail")]
        public void B_Step2()
        {
            Assert.That(count, Is.EqualTo(1));
            count++;
            Assert.That(count, Is.EqualTo(2));
        }

        [Test]
        [Category("ExpectedToFail")]
        public void A_Step3()
        {
            Assert.That(count, Is.EqualTo(2));
            count++;
            Assert.That(count, Is.EqualTo(3));
        }
    }

    // With ordering, the tests happen in order.

    [TestFixture]
    public class Order
    {
        private int count;

        [OneTimeSetUp]
        public void SetUp()
        {
            count = 0;
        }

        // Ordering 

        [Test]
        [Order(1)]
        public void C_Step1()
        {
            Assert.That(count, Is.EqualTo(0));
            count++;
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        [Order(2)]
        public void B_Step2()
        {
            Assert.That(count, Is.EqualTo(1));
            count++;
            Assert.That(count, Is.EqualTo(2));
        }

        [Test]
        [Order(3)]
        public void A_Step3()
        {
            Assert.That(count, Is.EqualTo(2));
            count++;
            Assert.That(count, Is.EqualTo(3));
        }
    }

    // NUnit 3.8 adds TestFixture ordering.

    [TestFixture]
    public static class OrderTestState
    {
        public static volatile int Count = 0;
    }

    [TestFixture]
    [Order(1)] // New for 3.8
    public class FixtureOrder1
    {
        [Test]
        [Order(1)]
        public void C_Step1()
        {
            Assert.That(OrderTestState.Count, Is.EqualTo(0));
            OrderTestState.Count++;
            Assert.That(OrderTestState.Count, Is.EqualTo(1));
        }

        [Test]
        [Order(2)]
        public void B_Step2()
        {
            Assert.That(OrderTestState.Count, Is.EqualTo(1));
            OrderTestState.Count++;
            Assert.That(OrderTestState.Count, Is.EqualTo(2));
        }

        [Test]
        [Order(3)]
        public void A_Step3()
        {
            Assert.That(OrderTestState.Count, Is.EqualTo(2));
            OrderTestState.Count++;
            Assert.That(OrderTestState.Count, Is.EqualTo(3));
        }
    }
    [TestFixture]
    [Order(2)] // New for 
    public class FixtureOrder2
    {
        [Test]
        [Order(1)]
        public void C_Step1()
        {
            Assert.That(OrderTestState.Count, Is.EqualTo(3));
            OrderTestState.Count++;
            Assert.That(OrderTestState.Count, Is.EqualTo(4));
        }

        [Test]
        [Order(2)]
        public void B_Step2()
        {
            Assert.That(OrderTestState.Count, Is.EqualTo(4));
            OrderTestState.Count++;
            Assert.That(OrderTestState.Count, Is.EqualTo(5));
        }

        [Test]
        [Order(3)]
        public void A_Step3()
        {
            Assert.That(OrderTestState.Count, Is.EqualTo(5));
            OrderTestState.Count++;
            Assert.That(OrderTestState.Count, Is.EqualTo(6));
        }
    }
}
