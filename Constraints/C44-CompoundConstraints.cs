using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit_Demonstration.Constraints
{
    [TestFixture]
    public class CompoundConstraints
    {
        [Test]
        public void Conjunction()
        {
            // There are conjunctions through chaining.
            Assert.That(55, Is.GreaterThan(45).And.LessThan(70));
            Assert.That(50, Is.LessThan(100).Or.GreaterThan(200));
            Assert.That(500, Is.LessThan(100).Or.GreaterThan(200));
        }

        [Test]
        public void ConjunctionOperators()
        {
            // Binary operators are overloaded
            Assert.That(55, Is.GreaterThan(45) & Is.LessThan(70));
            Assert.That(50, Is.LessThan(100) | Is.GreaterThan(200));
            Assert.That(500, Is.LessThan(100) | Is.GreaterThan(200));
        }

    }
}
