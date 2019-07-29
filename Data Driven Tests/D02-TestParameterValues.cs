using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace NUnit_Demonstration.Data_Driven_Tests
{
    [TestFixture]
    public class TestParameters
    {
        private bool Recognize(string text)
        {
            Regex regex = new Regex("[A-F][1-9]");
            return regex.IsMatch(text);
        }

        // You can list test values by hand.
        [Test]
        public void GeneratedValues_ByHand()
        {
            Assert.IsTrue(Recognize("A1"));
            Assert.IsTrue(Recognize("A2"));
            Assert.IsTrue(Recognize("B1"));
            Assert.IsTrue(Recognize("F1"));
            Assert.IsTrue(Recognize("F9"));
        }

        // You can use test cases.
        [TestCase("A1")]
        [TestCase("A2")]
        [TestCase("B1")]
        [TestCase("F1")]
        [TestCase("F9")]
        public void GeneratedValues_TestCases(string text)
        {
            Assert.IsTrue(Recognize(text));
        }

        // But this example is about values on parameters.
        [Test]
        public void GeneratedValues_Values(
            [Values("A1","A2","B1","F1","F9")] string text)
        {
            Assert.IsTrue(Recognize(text));
        }

        // Using the Sequential attribute, you can pair-up the values of two parameters.
        // This tests the same strings as the previous tests.
        [Test,Sequential]
        public void GeneratedValues_SequentialValues(
            [Values("A", "A", "B", "F", "F")] string letter,
            [Values("1", "2", "1", "1", "9")] string digit)
        {
            Assert.IsTrue(Recognize(letter+digit));
        }

        // The real power comes from using the Combinatorial attribute.
        // Here's all 54 combinations.
        [Test, Combinatorial]
        public void GeneratedValues_CombinatorialValues(
            [Values("A", "B", "C", "D", "E", "F")] string letter,
            [Values("1", "2", "3", "4", "5", "6", "7", "8", "9")] string digit)
        {
            Assert.IsTrue(Recognize(letter + digit));
        }

        // With numbers, you can generate them over a range.
        [Test, Combinatorial]
        public void GeneratedValues_Range(

            [Values("A", "B", "C", "D", "E", "F")] string letter,
            [Range(1,9,1)] int digit)
        {
            Assert.IsTrue(Recognize(letter + digit));
        }

        // You can also generate random data
        // e.g. test inversion of the square 10 times with random data
        // This is MUCH slower than doing it in a loop, since there's a
        // full setup and teardown each time.

        // Resharper 7 has trouble understanding this...
        [Test]
        public void GeneratedValues_VerifySqrt(
            [Random(0.0, 1000000, 10)] double sample)
        {
            Assert.AreEqual(sample, Math.Sqrt(sample*sample));
        }
    }
}
