using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit_Demonstration.Constraints
{
    // NUnit has comparison constraints.
    [TestFixture]
    partial class Comparison
    {
        [Test]
        public void CompareIntegers()
        {
            Assert.That(100, Is.LessThan(101));
            Assert.That(100, Is.LessThanOrEqualTo(100));
            Assert.That(100, Is.LessThanOrEqualTo(101));
            Assert.That(100, Is.GreaterThan(99));
            Assert.That(100, Is.GreaterThanOrEqualTo(100));
            Assert.That(100, Is.GreaterThanOrEqualTo(99));
        }

        [Test]
        public void CompareDoubles()
        {
            Assert.That(100.000, Is.LessThan(100.001));
            Assert.That(100.000, Is.LessThanOrEqualTo(100.000));
            Assert.That(100.000, Is.LessThanOrEqualTo(100.001));
            Assert.That(100.000, Is.GreaterThan(99.999));
            Assert.That(100.000, Is.GreaterThanOrEqualTo(100.000));
            Assert.That(100.000, Is.GreaterThanOrEqualTo(99.999));
        }

        [Test]
        public void CompareDecimal()
        {
            Assert.That(100.00m, Is.LessThan(100.01m));
            Assert.That(100.00m, Is.LessThanOrEqualTo(100.00m));
            Assert.That(100.00m, Is.LessThanOrEqualTo(100.01m));
            Assert.That(100.00m, Is.GreaterThan(99.99m));
            Assert.That(100.00m, Is.GreaterThanOrEqualTo(100.00m));
            Assert.That(100.00m, Is.GreaterThanOrEqualTo(99.99m));
        }

        [Test]
        public void CompareBinary()
        {
            Assert.That(true, Is.True);
            Assert.That(false, Is.False);
        }

        [Test]
        public void NullTest()
        {
            Assert.That(null, Is.Null);
            Assert.That("", Is.Not.Null);
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
            Assert.That(a, Is.GreaterThan(b));

            // Equality doesn't work.
            var c = new Thing() {WeightInGrams = 120};
            Assert.That(a, Is.Not.EqualTo(b));

            // Assertions would have had to overlap to show equality.
            Assert.That(a, Is.LessThanOrEqualTo(c));
            Assert.That(a, Is.GreaterThanOrEqualTo(c));

            // Constraints can do that this way.
            Assert.That(a, Is.LessThanOrEqualTo(c) | Is.GreaterThanOrEqualTo(c));
        }

        [Test]
        public void ComparableEqualityByConjunction()
        {
            // Equality doesn't work with IComparables
            var a = new Thing() {WeightInGrams = 120};
            var c = new Thing() {WeightInGrams = 120};
            Assert.That(a, Is.Not.EqualTo(c));

            // Assertions would have had to overlap to show equality.
            Assert.That(a, Is.LessThanOrEqualTo(c));
            Assert.That(a, Is.GreaterThanOrEqualTo(c));

            // Constraints can do that this way.
            Assert.That(a, Is.LessThanOrEqualTo(c) | Is.GreaterThanOrEqualTo(c));
        }
    }

    [TestFixture]
    class Comparison_ExpectedToFail
    {
        // But still no direct test for equality.
        [Test]
        [Category("ExpectedToFail")]
        public void ComparableEquality_ExpectedToFail()
        {
            var a = new Comparison.Thing() {WeightInGrams = 120};
            var c = new Comparison.Thing() {WeightInGrams = 120};
            Assert.That(a, Is.EqualTo(c));
        }
    }
    [TestFixture]
    partial class Comparison
    {
        // However, you can get it to work by forcing the IComparer.

        // Example object to show comparisons.
        public class ThingComparer : IComparer<Thing>
        {
            public int Compare(Thing x, Thing y)
            {
                return x.CompareTo(y);
            }
        }

        [Test]
        public void ComparableEqualityForced()
        {
            var a = new Thing() { WeightInGrams = 120 };
            var c = new Thing() { WeightInGrams = 120 };
            Assert.That(a, Is.EqualTo(c).Using(new ThingComparer()));
        }

    }
}
