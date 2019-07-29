using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace NUnit_Demonstration.Extensions
{
    [TestFixture]
    partial class ConstraintsAreClasses
    {
        // Here's the standard constraint.
        [Test]
        public void UsualAreEqual()
        {
            Assert.That(2 + 2, Is.EqualTo(4));
        }

        // We can take the object and use it instead.
        [Test]
        public void AreEqualIsAClass()
        {
            Constraint isEqualTo4 = Is.EqualTo(4);
            Assert.That(2 + 2, isEqualTo4);
        }

        // Or we could just call the constructors like any other object.
        [Test]
        public void ConstructableConstraints()
        {
            Constraint isEqualTo4 = new EqualConstraint(4);
            Assert.That(2 + 2, isEqualTo4);
        }

        // That's kind of nice because we can reuse it.
        [Test]
        public void ReusingConstraints()
        {
            var isEqualTo4 = new EqualConstraint(4);
            Assert.That(2 + 2, isEqualTo4);
            Assert.That(1 + 3, isEqualTo4);
            Assert.That(10 - 6, isEqualTo4);
        }
    }
    [TestFixture]
    class ConstraintsAreClasses_ExpectedToFail
    {
        // Clearly, the fluent syntax "Is...", "Has..." etc. is a convenience
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

            // If you look a the message, you'll see it lost track of the Not operator
        }
    }
    [TestFixture]
    partial class ConstraintsAreClasses
    {
        // Fortunately, NUnit provides a ReusableConstraint construct the makes it safe
        // to reuse constraints created by these expressions.
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

        // Alternatively, just ignore the fluent operators and build constraint objects.
        [Test]
        public void BuildConstraintsByHand()
        {
            // Create a constraint.
            Constraint isNotEqualTo4 = new NotConstraint(new EqualConstraint(4));

            // Constructed constraints are always resolved and always work correctly.
            Assert.That(5, isNotEqualTo4);
            Assert.That(6, isNotEqualTo4);
            Assert.That(-527, isNotEqualTo4);
        }

    }
}
