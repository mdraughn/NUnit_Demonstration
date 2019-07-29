using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit_Demonstration.Test_Cases
{
    [TestFixture]
    public class TestCaseData
    {
        // Let's test a simple math method:
        public int Divider(int a, int b)
        {
            return a / b;
        }

        // Here's one way to test several cases:
        [Test]
        public void TestCasesByHand()
        {
            Assert.AreEqual(5, Divider(10, 2));
            Assert.AreEqual(-5, Divider(10, -2));
            Assert.AreEqual(3, Divider(10, 3));
            Assert.AreEqual(0, Divider(0, 1));
            Assert.AreEqual(5866, Divider(492384274, 83927));
            Assert.Throws<DivideByZeroException>(() =>Divider(99, 0));
        }

        // Here's an easier way.
        [TestCase(10, 2, 5)]
        [TestCase(10, -2, -5)]
        [TestCase(10, 3, 3)]
        [TestCase(0, 1, 0)]
        [TestCase(492384274, 83927, 5866)]
        public void TestCasesWithParameters(int dividend, int divisor, int quotient)
        {
            Assert.AreEqual(quotient, Divider(dividend, divisor));
        }

        // But you still have to pick up the special case:
        [Test]
        public void TestCasesWithParametersDivByZero()
        {
            Assert.Throws<DivideByZeroException>(() => Divider(99, 0));
        }
        
        // Test cases support special parameters which allow you to specify results.
        [TestCase(10, 2, ExpectedResult = 5)]
        [TestCase(10, -2, ExpectedResult = -5)]
        [TestCase(10, 3, ExpectedResult = 3)]
        [TestCase(0, 1, ExpectedResult = 0)]
        //[TestCase(99, 0, ExpectedException = typeof(DivideByZeroException))]  // Not supported in 3.0
        public int TestCasesWithParametersAndResults(int dividend, int divisor)
        {
            return Divider(dividend, divisor);
        }
    }
}
