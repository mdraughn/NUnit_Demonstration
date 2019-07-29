using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit_Demonstration.Assertions
{
    // NUnit has comparison assertions.
    [TestFixture]
    class Comparison
    {
        [Test]
        public void CompareIntegers()
        {
            // Note that this is normal comparison order, not the usual expect/actual order.
            Assert.Less(100, 101);
            Assert.LessOrEqual(100, 100);
            Assert.LessOrEqual(100, 101);
            Assert.Greater(100, 99);
            Assert.GreaterOrEqual(100, 100);
            Assert.GreaterOrEqual(100, 99);
        }

        [Test]
        public void CompareDoubles()
        {
            // Note that this is normal comparison order, not the usual expect/actual order.
            Assert.Less(100.000, 100.001);
            Assert.LessOrEqual(100.000, 100.000);
            Assert.LessOrEqual(100.000, 100.001);
            Assert.Greater(100.000, 99.999);
            Assert.GreaterOrEqual(100.000, 100.000);
            Assert.GreaterOrEqual(100.000, 99.999);
        }

        [Test]
        public void CompareDecimal()
        {
            // Note that this is normal comparison order, not the usual expect/actual order.
            Assert.Less(100.00m, 100.01m);
            Assert.LessOrEqual(100.00m, 100.00m);
            Assert.LessOrEqual(100.00m, 100.01m);
            Assert.Greater(100.00m, 99.99m);
            Assert.GreaterOrEqual(100.00m, 100.00m);
            Assert.GreaterOrEqual(100.00m, 99.99m);
        }

        // Example object to show comparisons.
        public class Thing : IComparable
        {
            public int WeightInGrams { get; set; }

            public int CompareTo(object obj)
            {
                var apple = (Thing) obj;
                return WeightInGrams - apple.WeightInGrams;
            }
        }

        [Test]
        public void ImplementsComparable()
        {
            // Inequalities work with anything that implements IComparable.
            var a = new Thing() {WeightInGrams = 120};
            var b = new Thing() {WeightInGrams = 110};
            Assert.Greater(a, b);
        }

        [Test]
        public void ComparableEquality()
        {
            // Equality doesn't work with IComparables
            var a = new Thing() {WeightInGrams = 120};
            var c = new Thing() {WeightInGrams = 120};
            Assert.AreNotEqual(a, c);

            // Have to overlap inequalities to show equality.
            Assert.LessOrEqual(a, c);
            Assert.GreaterOrEqual(a, c);
        }
    }

    // Bare tests for equality fail with IComparables.
    [TestFixture]
    class Comparison_ExpectedToFail
    {

        [Test]
        [Category("ExpectedToFail")]
        public void ComparableEquality_ExpectedToFail()
        {
            var a = new Comparison.Thing() { WeightInGrams = 120 };
            var c = new Comparison.Thing() { WeightInGrams = 120 };
            Assert.AreEqual(a, c);
        }

    }
}
