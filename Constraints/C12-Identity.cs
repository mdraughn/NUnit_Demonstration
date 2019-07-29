using System;
using NUnit.Framework;

namespace NUnit_Demonstration.Constraints
{
    [TestFixture]
    public class Identity
    {
        [Test]
        public void SameObject()
        {
            // Two things are the only the same if they are the same object.
            var a = new object();
            var b = new object();
            Assert.That(a, Is.Not.SameAs(b));

            // Copies point to the same heap object.
            b = a;
            Assert.That(a, Is.SameAs(b));
        }

        [Test]
        public void SameValueType()
        {
            // Value typed variable can never be the same.
            Int32 a = 5;
            Int32 b = 5;
            Assert.That(a, Is.EqualTo(b));
            Assert.That(a, Is.Not.SameAs(b));

            // Copies are different objects.
            b = a;
            Assert.That(a, Is.Not.SameAs(b));
        }

        [Test]
        public void SameStrings()
        {
            // Strings are objects and have identity that way even when they contain the same characters.
            var a = new String('x', 3); // "xxx"
            var b = new String('x', 3); // "xxx"
            Assert.That(a, Is.EqualTo(b));
            Assert.That(a, Is.Not.SameAs(b));

            // Copies point to the same heap object.
            b = a;
            Assert.That(a, Is.SameAs(b));

            // String literals are string objects, but since the are immutable, the compiler
            // will reuse the same string object if the strings are identical.
            a = "xxx";
            b = "xxx";
            Assert.That(a, Is.SameAs(b));

            // The compiler is pretty good at recognizing constant expressions.
            a = "xxx";
            b = "x" + "x" + "x";
            Assert.That(a, Is.SameAs(b));
            ;
        }

    }
}
