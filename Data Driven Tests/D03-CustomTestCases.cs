using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NUnit_Demonstration.Test_Cases
{
    [TestFixture]
    public class CustomTestCases
    {
        // Let's test a simple math method:
        public int Divider(int a, int b)
        {
            return a / b;
        }

        // The test case mechanism allows us to do this:
        [TestCase(10, 2, 5)]
        [TestCase(10, -2, -5)]
        [TestCase(10, 3, 3)]
        [TestCase(0, 1, 0)]
        [TestCase(492384274, 83927, 5866)]
        public void TestCasesWithParameters(int dividend, int divisor, int quotient)
        {
            Assert.AreEqual(quotient, Divider(dividend, divisor));
        }

        // Then we need a separate test for the exceptional case.
        [Test]
        public void TestCasesWithParametersDivZero()
        {
            Assert.Throws<DivideByZeroException>(() => Divider(99, 0));
        }

        // If we want more control, we can generate those parameters as arrays of objects
        // with custom code like this:
        public class DividerParameterFactory
        {
            public static IEnumerable DividerTestCases
            {
                get
                {
                    // Generate a range of divisors and dividends to test.
                    for (int dividend = 0; dividend <= 25; dividend += 7) {
                        for (int divisor = 1; divisor <= 4; divisor++) {
                            
                            // Hand-calculate the expected result.
                            int quotient = dividend / divisor;

                            // Return parameters.
                            yield return new object[] { dividend, divisor, quotient };
                        }
                    }
                }
            }
        }

        // Now let's test with those parameters, specifying the factory class and passing the name of the enumerator property.
        [Test, TestCaseSource(typeof(DividerParameterFactory), "DividerTestCases")]
        public void TestCasesGenerateParameters(int dividend, int divisor, int quotient)
        {
            Assert.AreEqual(quotient, Divider(dividend, divisor));
        }

        // Still need a separate test for the exceptional case.
        [Test]
        public void TestCasesGenerateParametersDivZero()
        {
            Assert.Throws<DivideByZeroException>(() => Divider(99, 0));
        }

        // Or if we want better control over the results, with manual test cases, we do this:
        [TestCase(10, 2, ExpectedResult = 5)]
        [TestCase(10, -2, ExpectedResult = -5)]
        [TestCase(10, 3, ExpectedResult = 3)]
        [TestCase(0, 1, ExpectedResult = 0)]
        //[TestCase(99, 0, ExpectedException = typeof(DivideByZeroException))]  // Not supported in 3.0
        public int TestCasesWithParametersAndResults(int dividend, int divisor)
        {
            return Divider(dividend, divisor);
        }

        // To do that in a custom data generator, we have to implement an enumerable
        // that returns ITestCaseData objects. In this example, use the built-in TestCaseData class.
        public class DividerTestCaseDataFactory
        {
            public static IEnumerable<ITestCaseData> DividerTestCases
            {
                get
                {
                    // Generate a range of divisors and dividends to test.
                    for(int dividend = 0; dividend <= 25; dividend+=7)
                    {
                        for(int divisor = 1; divisor <= 4; divisor++)
                        {
                            // Hand-calculate the expected result.
                            int result = dividend/divisor;

                            // Generate a test case.
                            yield return new NUnit.Framework.TestCaseData(dividend, divisor).Returns(result);
                        }
                    }
                }
            }  
        }

        // Now let's test with those parameters, specifying the factory class and passing the name of the enumerator property.
        [Test, TestCaseSource(typeof(DividerTestCaseDataFactory), "DividerTestCases")]
        public int TestCasesGenerateTestData(int dividend, int divisor)
        {
            return Divider(dividend, divisor);
        }

        // You can mix the return types, yielding object arrays for the regular data
        // and ITestCaseData for the exception case.
        public class DividerMixedFactory
        {
            public static IEnumerable DividerTestCases
            {
                get
                {
                    // Generate a range of divisors and dividends to test.
                    for (int dividend = 0; dividend <= 25; dividend += 7) {
                        for (int divisor = 1; divisor <= 4; divisor++) {

                            // Hand-calculate the expected result.
                            int quotient = dividend / divisor;

                            // Return parameters.
                            yield return new object[] { dividend, divisor, quotient };
                        }
                    }
                }
            }
        }

        // Now let's test with the mixed data.
        [Test, TestCaseSource(typeof(DividerMixedFactory), "DividerTestCases")]
        public void TestCasesWithMixedTypes(int dividend, int divisor, int quotient)
        {
            Assert.AreEqual(quotient, Divider(dividend, divisor));
        }

    }
}
