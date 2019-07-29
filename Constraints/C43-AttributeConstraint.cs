using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NUnit_Demonstration.Constraints
{
    [TestFixture]
    public class AttributeConstraint
    {
        // Declare a custom attribute we can look at.
        [AttributeUsage(AttributeTargets.All)]
        private class ExampleAttribute : Attribute
        {
            public string Text { get; set; }
        }

        // Apply it to a class and its members.
        [Example(Text = "OnClass")]
        public class TargetClass
        {
            [Example(Text = "OnField")]
            public string AField;

            [Example(Text = "OnProperty")]
            public string AProperty { get; set; }

            [Example(Text = "OnMethod")]
            public string AMethod()
            {
                return "";
            }
        }

        // Assert that it's there.

        [Test]
        public void ClassHasAttribute()
        {
            Assert.That(typeof(TargetClass), Has.Attribute<ExampleAttribute>());
            Assert.That(typeof(TargetClass), Has.Attribute(typeof(ExampleAttribute)));
        }
        [Test]
        public void FieldHasAttribute()
        {
            Assert.That(typeof(TargetClass).GetField("AField"), Has.Attribute<ExampleAttribute>());
            Assert.That(typeof(TargetClass).GetField("AField"), Has.Attribute(typeof(ExampleAttribute)));
        }
        [Test]
        public void PropertyHasAttribute()
        {
            Assert.That(typeof(TargetClass).GetProperty("AProperty"), Has.Attribute<ExampleAttribute>());
            Assert.That(typeof(TargetClass).GetProperty("AProperty"), Has.Attribute(typeof(ExampleAttribute)));
        }
        [Test]
        public void MethodHasAttribute()
        {
            Assert.That(typeof(TargetClass).GetMethod("AMethod"), Has.Attribute<ExampleAttribute>());
            Assert.That(typeof(TargetClass).GetMethod("AMethod"), Has.Attribute(typeof(ExampleAttribute)));
        }

        // Check a property of the attribute.

        [Test]
        public void ClassAttributeProperty()
        {
            Assert.That(typeof(TargetClass), Has.Attribute<ExampleAttribute>().Property("Text").EqualTo("OnClass"));
            Assert.That(typeof(TargetClass), Has.Attribute(typeof(ExampleAttribute)).Property("Text").EqualTo("OnClass"));
        }
        [Test]
        public void FieldAttributeProperty()
        {
            Assert.That(typeof(TargetClass).GetField("AField"), Has.Attribute<ExampleAttribute>().Property("Text").EqualTo("OnField"));
            Assert.That(typeof(TargetClass).GetField("AField"), Has.Attribute(typeof(ExampleAttribute)).Property("Text").EqualTo("OnField"));
        }
        [Test]
        public void PropertyAttributeProperty()
        {
            Assert.That(typeof(TargetClass).GetProperty("AProperty"), Has.Attribute<ExampleAttribute>().Property("Text").EqualTo("OnProperty"));
            Assert.That(typeof(TargetClass).GetProperty("AProperty"), Has.Attribute(typeof(ExampleAttribute)).Property("Text").EqualTo("OnProperty"));
        }
        [Test]
        public void MethodAttributeProperty()
        {
            Assert.That(typeof(TargetClass).GetMethod("AMethod"), Has.Attribute<ExampleAttribute>().Property("Text").EqualTo("OnMethod"));
            Assert.That(typeof(TargetClass).GetMethod("AMethod"), Has.Attribute(typeof(ExampleAttribute)).Property("Text").EqualTo("OnMethod"));
        }

        // Also works on enums.

        [Example(Text = "OnEnum")]
        private enum TargetEnum
        {
            [Example(Text = "OnEnumValue")]
            ItemA
        }

        [Test]
        public void EnumHasAttribute()
        {
            Assert.That(typeof(TargetEnum), Has.Attribute<ExampleAttribute>());
            Assert.That(typeof(TargetEnum), Has.Attribute(typeof(ExampleAttribute)));
        }

        [Test]
        public void EnumAttributeProperty()
        {
            Assert.That(typeof(TargetEnum), Has.Attribute<ExampleAttribute>().Property("Text").EqualTo("OnEnum"));
            Assert.That(typeof(TargetEnum), Has.Attribute(typeof(ExampleAttribute)).Property("Text").EqualTo("OnEnum"));
        }

        [Test]
        public void EnumValueHasAttribute()
        {
            Assert.That(typeof(TargetEnum).GetField("ItemA"), Has.Attribute<ExampleAttribute>());
            Assert.That(typeof(TargetEnum).GetField("ItemA"), Has.Attribute(typeof(ExampleAttribute)));
        }
        [Test]
        public void EnumValueAttributeProperty()
        {
            Assert.That(typeof(TargetEnum).GetField("ItemA"), Has.Attribute<ExampleAttribute>().Property("Text").EqualTo("OnEnumValue"));
            Assert.That(typeof(TargetEnum).GetField("ItemA"), Has.Attribute(typeof(ExampleAttribute)).Property("Text").EqualTo("OnEnumValue"));
        }

    }
}
