using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit_Demonstration.Constraints
{
    [TestFixture]
    class StringConstraints
    {
        [Test]
        public void AreEqualString()
        {
            var foobar = "foobar";
            Assert.That(foobar, Is.EqualTo("foobar"));
        }
        [Test]
        public void EmptyString()
        {
            Assert.That("", Is.Empty);
            Assert.That("foobar", Is.Not.Empty);
        }

        [Test]
        public void RuntimeStrings()
        {
            // Runtime strings can be equal even though they're not the same object.
            var foobar = "foobar";
            var constructed = "foo";
            constructed += "bar";
            Assert.That(constructed, Is.EqualTo(foobar));
            Assert.That(constructed, Is.Not.SameAs(foobar));

            // Interning makes identical strings the same object.
            Assert.That(String.Intern(constructed), Is.SameAs(String.Intern(foobar)));
        }

        // Constraints have some chained options.

        [Test]
        public void CaseBlindComparison()
        {
            var expected = "Hello";
            var actual = "HEllo";

            // NUnit string equality is case-sensitive.
            Assert.That(actual, Is.Not.EqualTo(expected));

            // To get case sensitive, you could do this.
            Assert.That(actual.ToLower(), Is.EqualTo(expected.ToLower()));

            // But this is clearer and produces a better error message.
            Assert.That(actual, Is.EqualTo(expected).IgnoreCase);
        }

        [Test]
        public void StringContains()
        {
            var text = "the quick brown fox jumps over the fat lazy dog";

            // You could test for string containment this way:
            Assert.IsTrue(text.Contains("fox"));
            Assert.IsFalse(text.Contains("hen"));

            // But this is more direct and produces a better error message.
            Assert.That(text, Does.Contain("fox"));
            Assert.That(text, Does.Not.Contain("hen"));

            // With caseblind variant.
            Assert.That(text, Does.Contain("FOX").IgnoreCase);

            // There is alternate syntax.
            Assert.That(text, Contains.Substring("dog"));

            // But old-style string containment is deprecated.
            //Assert.That(text, Is.StringContaining("fox"));
            //Assert.That(text, Is.Not.StringContaining("hen"));

            // And a special syntax for conjunctions.
            Assert.That(text, Does.Contain("fox").And.Contain("dog"));
            Assert.That(text, Does.Contain("fox").And.Contains("dog"));
        }

        [Test]
        public void StringStartsWith()
        {
            // You can test starting substrings.
            var text = "four score and seven years ago";

            // Old-style string constraint is deprecated.
            //Assert.That(text, Is.StringStarting("four"));
            //Assert.That(text, Is.Not.StringStarting("five"));

            Assert.That(text, Does.StartWith("four"));
            Assert.That(text, Does.Not.StartWith("five"));

            // With caseblind variant.
            Assert.That(text, Does.StartWith("FouR").IgnoreCase);

            // Also has alternate syntax for conjunctions.
            Assert.That(text, Does.Contain("score").And.StartWith("four"));
            Assert.That(text, Does.Contain("score").And.StartsWith("four"));
        }

        [Test]
        public void StringEndsWith()
        {
            // You can test ending substrings.
            var text = "this is the end";

            // Old-style string constraint is deprecated.
            //Assert.That(text, Is.StringEnding("the end"));
            //Assert.That(text, Is.Not.StringEnding("fini"));

            Assert.That(text, Does.EndWith("the end"));
            Assert.That(text, Does.Not.EndWith("fini"));

            // With caseblind variant.
            Assert.That(text, Does.EndWith("The End").IgnoreCase);

            // Also has alternate syntax for conjunctions.
            Assert.That(text, Does.StartWith("this").And.EndWith("end"));
            Assert.That(text, Does.StartWith("this").And.EndsWith("end"));
        }

        [Test]
        public void StringMatchRegex()
        {
            var regex = "^[A-Z]{1,3}[1-9][0-9]{2}$";

            Assert.That("P100", Does.Match(regex));
            Assert.That("AB123", Does.Match(regex));
            Assert.That("ABC999", Does.Match(regex));

            Assert.That("ABC", Does.Not.Match(regex));
            Assert.That("100", Does.Not.Match(regex));
            Assert.That("ABC012", Does.Not.Match(regex));
            Assert.That("ABC1234", Does.Not.Match(regex));
            Assert.That("p100", Does.Not.Match(regex));

            // Of course, that last one could be caseblind.
            Assert.That("p100", Does.Match(regex).IgnoreCase);

            // And there's the alternative conjunctive syntax.
            Assert.That("AB123", Does.Match("A").And.Matches(regex));
            Assert.That("AB123", Does.Match("A").And.Matches(regex));
        }

    }
}
