using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit_Demonstration.Assertions
{
    [TestFixture]
    public class NumericAsserts
    {
        [Test]
        public void AreEqualInt()
        {
            int n = 2 + 2;
            Assert.AreEqual(4, n);
        }

        [Test]
        public void AreEqualDouble()
        {
            double ten = 10.0;
            Assert.AreEqual(10.0, ten);
        }

        [Test]
        public void DoublesConvert()
        {
            // There's some implicit conversion.
            double ten = 10.0;
            Assert.AreEqual(10, ten);
        }

        [Test]
        public void DoublesMathIsInexact()
        {
            // Many fractions cannot be represented accurately, which screws up the math.
            double oneTenth = 1.0 / 10.0;
            double oneHundreth = 1.0 / 100.0;
            Assert.IsFalse(oneHundreth == (oneTenth * oneTenth));

            // Normal comparison is unequal.
            Assert.AreNotEqual(oneHundreth, oneTenth * oneTenth);
            
            // Floating point comparison can be made soft (2.4.4).
            Assert.AreEqual(oneHundreth,oneTenth * oneTenth,0.000001);

            // You can also specify large amounts of slop for approximate comparisons.
            Assert.AreEqual(1.18, 1, 0.20);
        }

        [Test]
        public void NaNIsSpecial()
        {
            // (2.4.4)

            // The standards say that NaN is formally never equal to anything, even itself.
            // ReSharper disable once EqualExpressionComparison
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            #pragma warning disable CS1718
            Assert.False(double.NaN == double.NaN);

            // But for testing purposes they are considered equal, so you can tell NUnit to expect NaN.
            Assert.AreEqual(double.NaN, double.NaN);

            // But if checking for NaN, it's better to use an explicit test.
            Assert.IsNaN(double.NaN);
        }
    }
}
