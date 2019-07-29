using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit_Demonstration.Assertions
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
            Assert.AreNotSame(a, b);

            // Copies point to the same heap object.
            b = a;
            Assert.AreSame(a, b);
        }

        [Test]
        public void SameValueType()
        {
            // Value typed variable can never be the same.
            Int32 a = 5;
            Int32 b = 5;
            Assert.AreEqual(a, b);
            Assert.AreNotSame(a, b);

            // Copies are different objects.
            b = a;
            Assert.AreNotSame(a, b);
        }

        [Test]
        public void SameStrings()
        {
            // Strings are objects and have identity that way even when they contain the same characters.
            var a = new String('x', 3); // "xxx"
            var b = new String('x', 3); // "xxx"
            Assert.AreEqual(a, b);
            Assert.AreNotSame(a, b);

            // Copies point to the same heap object.
            b = a;
            Assert.AreSame(a, b);

            // String literals are string objects, but since the are immutable, the compiler
            // will reuse the same string object if the strings are identical.
            a = "xxx";
            b = "xxx";
            Assert.AreSame(a, b);

            // The compiler is pretty good at recognizing constant expressions.
            a = "xxx";
            b = "x" + "x" + "x";
            Assert.AreSame(a, b);
        }

    }
}
