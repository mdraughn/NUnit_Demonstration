using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit_Demonstration.Constraints
{
    [TestFixture]
    public class PropertyConstraint
    {
        public class Target
        {
            public int Size { get; set; }
            public double Speed;
            public double Temperature { get; set; }
            private decimal Cost { get; set; }
        }

        [Test]
        public void HasProperty()
        {
            Target target = new Target();
            Assert.That(target, Has.Property("Size"));
        }

        [Test]
        public void PrivateProperty()
        {
            Target target = new Target();
            Assert.That(target, Has.Property("Cost"));
        }

        [Test]
        public void NotFields()
        {
            // Fields aren't properties.
            Target target = new Target();
            Assert.That(target, Has.No.Property("Speed"));
        }

        [Test]
        public void AdditionalConstraints()
        {
            Target target = new Target();
            target.Size = 10;
            target.Temperature = 25.5;
            Assert.That(target, Has.Property("Size").EqualTo(10));
            Assert.That(target, Has.Property("Temperature").InRange(25.0, 26.0));
        }

        [Test]
        public void KnownProperties()
        {
            // Some well-known properties have aliases.
            string hello = "hello";
            Assert.That(hello, Has.Property("Length").EqualTo(5));
            Assert.That(hello, Has.Length.EqualTo(5));

            List<int> list = new List<int>() { 1, 2, 3 };
            Assert.That(list, Has.Property("Count").LessThan(4));
            Assert.That(list, Has.Count.LessThan(4));

            var e = new Exception("Oops", new ArithmeticException());
            Assert.That(e, Has.Property("Message").EqualTo("Oops"));
            Assert.That(e, Has.Message.EqualTo("Oops"));
            Assert.That(e, Has.Property("InnerException").InstanceOf<ArithmeticException>());
            Assert.That(e, Has.InnerException.InstanceOf<ArithmeticException>());
        }

    }
}
