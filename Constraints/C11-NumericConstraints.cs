using System;
using NUnit.Framework;

namespace NUnit_Demonstration.Constraints
{
    [TestFixture]
    public class NumericConstraints
    {
        [Test]
        public void AreEqualInt()
        {
            int n = 2 + 2;
            Assert.That(n,Is.EqualTo(4));
        }

        [Test]
        public void AreEqualDouble()
        {
            double ten = 10.0;
            Assert.That(ten, Is.EqualTo(10.0));
        }

        [Test]
        public void DoublesConvert()
        {
            // There's some implicit conversion.
            double ten = 10.0;
            Assert.That(ten, Is.EqualTo(10));
        }

        [Test]
        public void DoublesMathIsInexact()
        {
            // Many fractions cannot be represented accurately, which screws up the math.
            double oneTenth = 1.0 / 10.0;
            double oneHundreth = 1.0 / 100.0;
            Assert.That(oneHundreth != (oneTenth * oneTenth));

            // Normal comparison is unequal.
            Assert.That((oneTenth*oneTenth), Is.Not.EqualTo(oneHundreth));

            // Floating point comparison can be made soft.
            Assert.That((oneTenth*oneTenth), Is.EqualTo(oneHundreth).Within(0.000001));
            Assert.That((oneTenth*oneTenth), Is.EqualTo(oneHundreth).Within(0.1).Percent);

            // You can also specify large amounts of slop for approximate comparisons.
            Assert.That(1.18, Is.EqualTo(1).Within(0.20));

            // With constraints, you can also specify slop as a percentage.
            Assert.That(1.18, Is.EqualTo(1).Within(20).Percent);

            // That's nice because it scales as a ratio.
            Assert.That(0.000000118, Is.EqualTo(0.0000001).Within(20).Percent);
            Assert.That(0.0000118, Is.EqualTo(0.00001).Within(20).Percent);
            Assert.That(0.00118, Is.EqualTo(0.001).Within(20).Percent);
            Assert.That(0.0118, Is.EqualTo(0.01).Within(20).Percent);
            Assert.That(0.118, Is.EqualTo(0.1).Within(20).Percent);
            Assert.That(1.18, Is.EqualTo(1).Within(20).Percent);
            Assert.That(11.8, Is.EqualTo(10).Within(20).Percent);
            Assert.That(118, Is.EqualTo(100).Within(20).Percent);
            Assert.That(1180, Is.EqualTo(1000).Within(20).Percent);
            Assert.That(11800, Is.EqualTo(10000).Within(20).Percent);
            Assert.That(1180000, Is.EqualTo(1000000).Within(20).Percent);
            Assert.That(118000000, Is.EqualTo(100000000).Within(20).Percent);

            // You can also specify precision in Units of Least Precision, which roughly
            // means that it must be the specified value or one of the adjacent floating
            // point numbers.
            Assert.That((oneTenth * oneTenth), Is.EqualTo(oneHundreth).Within(1).Ulps);
            Assert.That(1.0000000000000001, Is.EqualTo(1.0000000000000000).Within(1).Ulps);
            Assert.That(1.0000000000000002, Is.EqualTo(1.0000000000000000).Within(1).Ulps);
            Assert.That(1.0000000000000003, Is.EqualTo(1.0000000000000000).Within(1).Ulps);
            Assert.That(1.0000000000000004, Is.Not.EqualTo(1.000000000000000).Within(1).Ulps);
        }

        [Test]
        public void NaNIsSpecial()
        {
            // (2.4.4)

            // The standards say that NaN is formally never equal to anything, even itself.
            // ReSharper disable once EqualExpressionComparison
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            #pragma warning disable CS1718
            Assert.That(Double.NaN != Double.NaN);

            // But for testing purposes they are considered equal, so you can tell NUnit to expect NaN.
            Assert.That(Double.NaN,Is.EqualTo(Double.NaN));

            // But if checking for NaN, it's better to use an explicit test.
            Assert.That(Double.NaN, Is.NaN);
        }
    }
}
