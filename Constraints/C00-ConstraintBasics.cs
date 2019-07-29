using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace NUnit_Demonstration.Constraints
{
    [TestFixture]
    partial class ConstraintBasics
    {
        // Constraints are classes that decide if an object meets some criterion.
        [Test]
        public void SimpleEqualityConstraint()
        {
            Constraint constrainedEqualTo999 = new EqualConstraint(expected: 999);
            int actual = 999;
            Assert.That(actual, constrainedEqualTo999);
        }

        // Certain constraints allow composition in the constructor.
        [Test]
        public void NegatedEqualityConstraint()
        {
            Constraint constrainedNotFalse = new NotConstraint(new EqualConstraint(expected: false));
            bool actual = true;
            Assert.That(actual, constrainedNotFalse);
        }

        // That includes constraints joined by conjunctions.
        [Test]
        public void ConjoinedConstraints()
        {
            Constraint constrainedInRange = new AndConstraint(
                new GreaterThanConstraint(800),
                new LessThanConstraint(900));
            int actual = 850;
            Assert.That(actual, constrainedInRange);

            // This case can be simplified by using the constraint for ranges.
            Assert.That(actual, new RangeConstraint(800, 900));
        }

        // Those constructors are flexible, but not very concise, so NUnit has a fluent notation.

        [Test]
        public void Notation()
        {
            // Constraint notation uses a single method and a constraint class called Is (or Has or Does or a few other special cases).
            // (They are more natural to read than assertions, but harder to type.)

            // Here are the preceding assertions in fluent form.
            Assert.That(999, Is.EqualTo(999));
            Assert.That(true, Is.Not.EqualTo(false));
            Assert.That(850, Is.GreaterThan(800).And.LessThan(900));
            Assert.That(850, Is.InRange(800, 900));
        }
    }

    namespace NUnit_Demonstration.Constraints
    {
        [TestFixture]
        [Category("ExpectedToFail")]
        class ConstraintBasics_ExpectedToFail
        {
            // Clearly, the fluent syntax "Is...", "Has...", "Does..." etc. is a convenience
            // for constructing constraints, but it has a danger because it can
            // return constraints that are not fully resolved when the expression
            // involves operators such as "Not".
            [Test, Category("ExpectedToFail")]
            public void DangerousReuseOfConstraints_ExpectedToFail()
            {
                // Create a constraint.
                Constraint isNotEqualTo4 = Is.Not.EqualTo(4);

                // This one will resolve and succeed.
                Assert.That(5, isNotEqualTo4);

                // The problem is that resolving the constraint will have lost the original
                // definition, causing it to fail if reused.
                Assert.That(6, isNotEqualTo4);

                // If you look at the message, you'll see it lost track of the Not operator
            }
        }
    }

    namespace NUnit_Demonstration.Constraints
    {
        [TestFixture]
        partial class ConstraintBasics
        {
            // One way to solve this is to go back to constructing constraints by hand:
            [Test]
            public void ReuseOfConstructedConstraints()
            {
                // Create a constraint.
                Constraint isNotEqualTo4 = new NotConstraint(new EqualConstraint(4));

                // This one will resolve and succeed.
                Assert.That(5, isNotEqualTo4);

                // And so will this.
                Assert.That(6, isNotEqualTo4);

                // If you look at the message, you'll see it lost track of the Not operator
            }

            // If you'd prefer to keep the fluent notation, NUnit provides a ReusableConstraint
            // construct the makes it safe to reuse constraints created by these expressions.
            // It has a very low cost, so there's no harm in using it everywhere you need it.
            [Test]
            public void ReusableConstraints()
            {
                // Create a constraint.
                ReusableConstraint isNotEqualTo4 = Is.Not.EqualTo(4);

                // This one will resolve and succeed.
                Assert.That(5, isNotEqualTo4);

                // This one will now succeed too.
                Assert.That(6, isNotEqualTo4);
            }
        }
    }
}