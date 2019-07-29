using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit_Demonstration.Assertions
{
    [TestFixture]
    class StringAsserts
    {
        [Test]
        public void AreEqualString()
        {
            var foobar = "foobar";
            Assert.AreEqual("foobar", foobar);
        }

        [Test]
        public void RuntimeStrings()
        {
            // Runtime strings can be equal even though they're not the same object.
            var foobar = "foobar";
            var constructed = "foo";
            constructed += "bar";
            Assert.AreEqual(foobar, constructed);
            Assert.AreNotSame(foobar, constructed);

            // Interning makes identical strings the same object.
            Assert.AreSame(String.Intern(foobar),String.Intern(constructed));
        }

        // For additional string assertions, use the StringAssert class (2.2.3).

        [Test]
        public void CaseBlindComparison()
        {
            var expected = "Hello";
            var actual = "HEllo";

            // NUnit string equality is case-sensitive.
            Assert.AreNotEqual(expected,actual);

            // To get case sensitive, you could do this.
            Assert.AreEqual(expected.ToLower(),actual.ToLower());

            // But this is clearer and produces a better error message.
            StringAssert.AreEqualIgnoringCase(expected,actual);
        }

        [Test]
        public void StringContains()
        {
            var text = "the quick brown fox jumps over the fat lazy dog";

            // You could test for string containment this way:
            Assert.IsTrue(text.Contains("fox"));
            Assert.IsFalse(text.Contains("hen"));

            // But this is clearer and produces a better error message.
            StringAssert.Contains("fox", text);
            StringAssert.DoesNotContain("hen",text);
        }

        [Test]
        public void StringStartsWith()
        {
            // You can test starting substrings.
            var text = "four score and seven years ago";
            StringAssert.StartsWith("four", text);
            StringAssert.DoesNotStartWith("five",text);
        }

        [Test]
        public void StringEndWith()
        {
            // You can test ending substrings.
            var text = "this is the end";
            StringAssert.EndsWith("the end", text);
            StringAssert.DoesNotEndWith("fini",text);
        }

        [Test]
        public void StringMatchRegex()
        {
            var regex = "^[A-Z]{1,3}[1-9][0-9]{2}$";

            StringAssert.IsMatch(regex, "P100");
            StringAssert.IsMatch(regex, "AB123");
            StringAssert.IsMatch(regex, "ABC999");
            StringAssert.DoesNotMatch(regex, "ABC");
            StringAssert.DoesNotMatch(regex, "100");
            StringAssert.DoesNotMatch(regex, "ABC012");
            StringAssert.DoesNotMatch(regex, "ABC1234");
            StringAssert.DoesNotMatch(regex, "p100");
        }

    }
}
