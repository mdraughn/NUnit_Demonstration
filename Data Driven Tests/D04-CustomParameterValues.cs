using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace NUnit_Demonstration.Data_Driven_Tests
{
    [TestFixture]
    public class CustomParameterValues
    {
        private bool Recognize(string text)
        {
            Regex regex = new Regex("[A-F][1-9]");
            return regex.IsMatch(text);
        }

        // You can always list the values.
        [Test]
        public void Parameters_ValuesByHand(
            [Values("A1","A2","B1","F1","F9")] string text)
        {
            Assert.IsTrue(Recognize(text));
        }

        // But as with test cases, we can generate the parameter values using a custom method.
        public class ParameterValuesFactory
        {
            public static IEnumerable<string> NextHexChar
            {
                get
                {
                    foreach (char letter in "ABCDEF")
                    {
                        for (int digit = 1; digit <= 9; digit++)
                        {
                            yield return String.Format("{0}{1}",letter,digit);
                        }
                    }
                }
            }
        }

        // Now let's test with that parameter generator, specifying the factory class
        // and passing the name of the enumerator property.
        [Test]
        public void Parameters_CustomValues(
            [ValueSource(typeof(ParameterValuesFactory), "NextHexChar")] string text)
        {
            Assert.IsTrue(Recognize(text));
        }

        // We could also do a split generation.
        public class ParameterValuesFactorySplit
        {
            public static IEnumerable<string> NextLetter
            {
                get
                {
                    foreach (char letter in "ABCDEF") {
                        yield return letter.ToString();
                    }
                }
            }

            public static IEnumerable<string> NextDigit
            {
                get
                {
                    for (int digit = 1; digit <= 9; digit++) {
                        yield return digit.ToString();
                    }
                }
            }
        }

        // Now combine them in all combinations.
        [Test, Combinatorial] public void Parameters_SplitCombinationValues(
            [ValueSource(typeof(ParameterValuesFactorySplit), "NextLetter")] string letter,
            [ValueSource(typeof(ParameterValuesFactorySplit), "NextDigit")] string digit)
        {
            Assert.IsTrue(Recognize(letter + digit));
        }

    }
}
